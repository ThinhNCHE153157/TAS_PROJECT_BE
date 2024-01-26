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
            if (request!=null)
            {
            return Ok(result);
            }
            return NotFound();
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
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> CreateTestForCourse([FromForm] CreateTestForCourseRequestDto request)
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

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> SubmitTest([FromBody] SaveTestResultRequestDto request)
        {
            var result = await _testService.SaveTestResult(request).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> TestResultDetail([FromQuery] int testId, int accountId)
        {
            var result = await _testService.TestDetail(testId, accountId).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetTestResultByAccount([FromQuery] int accountId)
        {
            var result = await _testService.GetTestResult(accountId).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> DeleteTest([FromQuery] int testId)
        {
            var result = await _testService.DeleteTest(testId).ConfigureAwait(false);
            return Ok(result);
        }
    }
}
