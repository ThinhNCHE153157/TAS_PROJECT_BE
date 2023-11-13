using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAS.Application.Services;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;

namespace TAS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly IAccountService _accountService;
        public ClassController(IClassService classService, IAccountService accountService)
        {
            _classService = classService;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClassByStudentId(int studentId) 
        {
            var data = await _classService.GetClassByStudentId(studentId);
            if(data == null)
            {
                return BadRequest();
            }
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetClassCode()
        {
            var data = await _classService.GetClassCode();
            if (data ==null)
            {
                return BadRequest();
            }
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentIntoClass(int accountId, string classCode)
        {
            var account = await _accountService.GetAccountByIdReturnAcc(accountId);
            if (account == null)
            {
                return NotFound($"Account with ID {accountId} not found.");
            }
            else
            {
                var isSuccess = await _classService.AddStudentIntoClass(classCode, accountId);
                if (!isSuccess)
                {
                    return BadRequest("Something wrong when add student into class");
                }

                return Ok();
            }
        }
    }
}
