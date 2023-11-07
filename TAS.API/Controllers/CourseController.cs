using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;

namespace TAS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ITestService _testService;

        public CourseController(ICourseService courseService, ITestService testService)
        {
            _courseService = courseService;
            _testService = testService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCourse(AddCourseRequestDto request)
        {
            var result = await _courseService.AddCourse(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseHomePage()
        {
            var result = await _courseService.getCourseHomepage();
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllCourse()
        {
            var result = await _courseService.GetAllCourse();
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCourseById([FromQuery] int id)
        {
            var result = await _courseService.GetCourseById(id);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CourseResult([FromQuery] int id)
        {
            var result = await _testService.CourseResult(id);
            return Ok(result);
        }
    }
}
