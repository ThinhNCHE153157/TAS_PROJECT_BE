using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAS.Application.Services.Interfaces;

namespace TAS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTestHomePage()
        {
            var result = await _testService.GetTestHomepage();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTest()
        {
            var result = await _testService.GetAllTest();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTestById([FromQuery] int id)
        {
            var result = await _testService.GetTestById(id);
            return Ok(result);
        }
    }
}
