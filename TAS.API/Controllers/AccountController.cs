﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.Entities;
using static TAS.Infrastructure.Enums.SystemEnum;

namespace TAS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        private readonly IMailService _mailService;
        private readonly ILogger _logger;

        public AccountController(IAccountService accountService, ITokenService tokenService, IMailService mailService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _mailService = mailService;
        }
        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns>Get all user</returns>
        [HttpGet]

        public async Task<IActionResult> GetAccountManagement()
        {
            var data = await _accountService.GetAccountManagement();
            return Ok(data);
        }
        [HttpGet]
        public async Task<IActionResult> getUser()
        {
            var data = await _accountService.GetAccounts();
            return Ok(data);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAccountById([FromQuery] int id)
        {
            var data = await _accountService.GetAccountById(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterRequestDto request)
        {
            var user = await _accountService.GetUserByEmail(request.Email);
            var user1 = await _accountService.GetAccountByUsername(request.Username);
            if (user != null || user1!=null)
            {
                return BadRequest("Email is already exist");
            }
            var isSuccess = await _accountService.UserRegister(request).ConfigureAwait(false);
            if (isSuccess)
            {
                await _mailService.SendVerifyCode(request.Email);
                return Ok();
            }
            if (!isSuccess)
            {
                return BadRequest("Something wrong when register");
            }

            return BadRequest("Something wrong when register");
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginRequestDto userLogin)
        {
            var UserAccount = await _accountService.UserLogin(userLogin).ConfigureAwait(false);
            if (UserAccount is null)
            {
                return Unauthorized("Wrong user name or password!");
            }
            var listRole = new List<string>();
            foreach (var role in UserAccount.Roles)
            {
                listRole.Add(role.RoleName);
            }
            string name = userLogin.UserName;
            if (listRole.Contains("Enterprise"))
            {
                name = _accountService.GetEnterpriseNameById(UserAccount.AccountId);
            }
            //var userRole = (UserRoles)UserAccount.Roles.RoleId;
            var authClaims = new Collection<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name,name),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name,name.ToString()),
            };
            foreach (var role in listRole)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var accessToken = _tokenService.GenerateAccessToken(authClaims);
            return Ok(new UserLoginResponseDto(UserAccount.AccountId, accessToken));
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] AccountAddRequestDto request)
        {
            var isSuccess = await _accountService.AddUser(request).ConfigureAwait(false);
            if (!isSuccess)
            {
                return BadRequest("Something wrong when add account");
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount(int accountId, [FromBody] AccountAddRequestDto request)
        {
            var account = await _accountService.GetAccountByIdReturnAcc(accountId);
            if (account == null)
            {
                return NotFound($"Account with ID {accountId} not found.");
            }
            else
            {
                var isSuccess = await _accountService.UpdateUser(request, accountId).ConfigureAwait(false);
                if (!isSuccess)
                {
                    return BadRequest("Something wrong when edit account");
                }

                return Ok();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                MailRequestDto mail = new MailRequestDto();
                mail.ToEmail = email;
                mail.Subject = "Reset Password";
                mail.Body = "Mã reset password của bạn là: 1234";
                await _accountService.SendEmailAsync(mail);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAllTeacher()
        {
            var data = await _accountService.GetAllTeacher();
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
        {
            var account = await _accountService.GetAccountById(request.AccountId);
            if (account == null)
            {
                return NotFound($"Account with ID {request.AccountId} not found.");
            }
            else
            {
                var isSuccess = await _accountService.ChangePassword(request).ConfigureAwait(false);
                if (!isSuccess)
                {
                    return BadRequest("Something wrong when register");
                }

                return Ok();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAvatar([FromForm] UpdateAvatarRequestDto request)
        {
            var isSuccess = await _accountService.UpdateAvatar(request).ConfigureAwait(false);
            if (!isSuccess)
            {
                return BadRequest("Something wrong when Upload Avatar");
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAvatar([FromQuery] int accountId)
        {
            var account = await _accountService.GetAccountById(accountId);
            if (account == null)
            {
                return NotFound($"Account with ID {accountId} not found.");
            }
            else
            {
                var isSuccess = await _accountService.DeleteAvatar(accountId).ConfigureAwait(false);
                if (!isSuccess)
                {
                    return BadRequest("Something wrong when Delete Avatar");
                }

                return Ok(isSuccess);
            }
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAllEnterprise()
        {
            var data = await _accountService.GetAllEnterprise();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyAccount([FromQuery] string otp, [FromQuery] string email)
        {
            Account account = await _accountService.GetUserByEmail(email);
            if (account == null)
            {
                return NotFound($"Account with Email {email} not found.");
            }
            if (account.Otpexpiretime < System.DateTime.Now)
            {
                return BadRequest("OTP is expired");
            }
            var isSuccess = await _accountService.VerifyAccount(otp, email).ConfigureAwait(false);
            if (!isSuccess)
            {
                return BadRequest("Something wrong when verify account");
            }
            return Ok("Verify successfully");
        }
        [HttpPost]
        public async Task<IActionResult> ResendSendVerifyCode([FromQuery] string email)
        {
            Account account = await _accountService.GetUserByEmail(email);
            if (account == null)
            {
                return NotFound($"Account with Email {email} not found.");
            }
            if (account.IsVerified == true)
            {
                return BadRequest("Account is verified");
            }
            else
            {
                await _mailService.SendVerifyCode(email);
                return Ok();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequestDto request)
        {
            if (request!=null)
            {
                Account acc = await _accountService.GetAccountByIdReturnAcc(request.AccountId);
                if (acc == null)
                {
                    return NotFound($"Account with ID {request.AccountId} not found.");
                }
                else
                {
                    var isSuccess = await _accountService.UpdateProfile(request).ConfigureAwait(false);
                    if (!isSuccess)
                    {
                        return BadRequest("Something wrong when update profile");
                    }

                    return Ok();
                }
            }
            return BadRequest("Something wrong when update profile");
        }

        [HttpPost]
        public  async Task<IActionResult> AddEnterprise([FromBody] AddEnterpriseRequestDto request)
        {
            var isSuccess = await _accountService.AddEnterprise(request).ConfigureAwait(false);
            if (!isSuccess)
            {
                return BadRequest("Something wrong when add enterprise");
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> changeStatusEnterprise([FromQuery] int accountId, [FromQuery] int status)
        {
            var isSuccess = await _accountService.changeStatusEnterprise(accountId, status).ConfigureAwait(false);
            if (!isSuccess)
            {
                return BadRequest("Something wrong when change status enterprise");
            }
            return Ok();
        }
    }
}
