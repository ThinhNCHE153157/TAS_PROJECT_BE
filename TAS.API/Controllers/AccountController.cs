using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF.Repositories;
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
        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
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
        public async Task<IActionResult> GetAccountById([FromQuery] int id)
        {
            var data = await _accountService.GetAccountById(id);
            return Ok(data);
        }

		[HttpPost]
        public async Task<IActionResult> UserRegister([FromBody] UserRegisterRequestDto request)
        {
            var isSuccess = await _accountService.UserRegister(request).ConfigureAwait(false);
            if (!isSuccess)
            {
                return BadRequest("Something wrong when register");
            }

            return Ok();
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
            //var userRole = (UserRoles)UserAccount.Roles.RoleId;

            var authClaims = new Collection<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name,userLogin.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            foreach (var role in listRole)
            {
                authClaims.Add( new Claim(ClaimTypes.Role, role));
            }
            var accessToken = _tokenService.GenerateAccessToken(authClaims);
            return Ok(new UserLoginResponseDto(UserAccount.AccountId, accessToken));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts_Manage()
        {
            var data = await _accountService.GetAllAccounts_Manage();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody] AccountAddRequestDto request)
        {
            var isSuccess = await _accountService.AddUser(request).ConfigureAwait(false);
            if (!isSuccess)
            {
                return BadRequest("Something wrong when register");
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditAccount(int accountId, [FromBody] AccountAddRequestDto request)
        {
            var account = await _accountService.GetAccountById(accountId);
            if (account == null)
            {
                return NotFound($"Account with ID {accountId} not found.");
            }
            else
            {
                var isSuccess = await _accountService.UpdateUser(request, accountId).ConfigureAwait(false);
                if (!isSuccess)
                {
                    return BadRequest("Something wrong when register");
                }

                return Ok();
            }
        }

        private string GenerateRandomVerificationCode()
        {
            Random random = new Random();
            int code = random.Next(1000, 10000);
            return code.ToString();
        }


        private void SendVerificationEmail(string recipient, string verificationCode)
        {
            var smtpClient = new SmtpClient("your-smtp-server.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("your-email@example.com", "your-email-password"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("your-email@example.com"),
                Subject = "The password reset verification code.",
                Body = $"Your verification code is: {verificationCode}",
            };

            mailMessage.To.Add(recipient);

            smtpClient.Send(mailMessage);
        }


        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {

            var account = await _accountService.GetUserByEmail(request.Email);
            if (account == null)
            {
                return BadRequest("The email does not exist in the system.");
            }

            var token = GenerateRandomVerificationCode();
            account.VerificationToken = token;
            account.VerificationTokenExpiry = DateTime.UtcNow.AddHours(1);

            SendVerificationEmail(account.Email, token);

            return Ok("Please check your email to receive the verification code.");
        }

    }
}
