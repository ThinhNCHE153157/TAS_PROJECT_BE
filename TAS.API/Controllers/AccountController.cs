using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
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
        public AccountController( IAccountService accountService, ITokenService tokenService)
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
            var  UserAccount = await _accountService.UserLogin(userLogin).ConfigureAwait(false);
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
    }
}
