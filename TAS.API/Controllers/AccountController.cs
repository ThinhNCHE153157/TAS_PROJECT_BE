using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;
using TAS.Data.Entities;

namespace TAS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController( IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        /// Get all user
        /// </summary>
        /// <returns>Get all user</returns>
        [HttpGet]
        public async Task<IActionResult> getUser()
        {
            var data = await _accountService.GetAccounts();
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
    }
}
