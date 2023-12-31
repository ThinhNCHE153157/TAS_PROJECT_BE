﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;
using TAS.Infrastructure.Helpers;

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
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [DisableRequestSizeLimit]
        //[Authorize]
        public async Task<IActionResult> AddCourse([FromForm] AddCourseRequestDto request)
        {
            var result = await _courseService.AddCourse(request);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> RequestCourse([FromBody] UpdateStatusRequestDto request)
        {
            var result = await _courseService.UpdateStatus(request.CourseId, request.Status).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCourseHomePage()
        {
            var result = await _courseService.getCourseHomepage();
            return Ok(result);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetAllCourse()
        {
            var result = await _courseService.GetAllCourse();
            return Ok(result);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetCourseById([FromQuery] int id)
        {
            var result = await _courseService.GetCourseById(id);
            return Ok(result);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> CourseResult([FromQuery] int id)
        {
            var result = await _testService.CourseResult(id);
            if (result!=null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> UpdateCost([FromBody] UpdateCostRequestDto request)
        {
            var result = await _courseService.UpdateCost(request).ConfigureAwait(false);
            if (result == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetCourseIdByName([FromQuery] string name)
        {
            var result = await _courseService.GetCourseIdByName(name);
            if (result != 0)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
