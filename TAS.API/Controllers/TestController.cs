using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;

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
        //[Authorize]
        public async Task<IActionResult> GetAllTest()
        {
            var result = await _testService.GetAllTest().ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTestById([FromQuery] GetTestByIdRequestDto request)
        {
            var result = await _testService.GetTestById(request.TestId);
            return Ok(result);
        }

        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> UpdateTest([FromBody] UpdateTestRequestDto request)
        {
            var result = await _testService.UpdateTest(request);
            return Ok(result);
        }
        [HttpDelete]
        //[Authorize]
        public async Task<IActionResult> UpdateStatus([FromQuery] UpdateStatusTestDto request)
        {
            var result = await _testService.UpdateStatusTest(request.TestId);
            return Ok(result);
        }
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CreateTestForCourse([FromBody] CreateTestForCourseRequestDto request)
        {
            var result = await _testService.CreateTestForCourse(request);
            return Ok(result);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> getListTestFree()
        {
            var result = await _testService.getListTestFreeResponseDtos();
            return Ok(result);
        }
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> getListPartOfTest([FromQuery] int request)
        {
            var result = await _testService.getListPartOfTest(request).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
