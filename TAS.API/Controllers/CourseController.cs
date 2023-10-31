using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAS.Application.Services.Interfaces;

namespace TAS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseHomePage()
        {
            var result = await _courseService.getCourseHomepage();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourse()
        {
            var result = await _courseService.GetAllCourse();
            return Ok(result);
        }
    }
}
