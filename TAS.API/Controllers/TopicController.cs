using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;

namespace TAS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;
        private readonly ITestService _testService;

        public TopicController(ITopicService topicService, ITestService testService)
        {
            _topicService = topicService;
            _testService = testService;
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> AddTopic([FromBody]AddTopicRequestDto request)
        {
            var result = await _topicService.addTopic(request);
            return Ok(result);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetTopicByCourseId([FromQuery]int courseid)
        {
            var result = await _topicService.getListTopicByCourseId(courseid);
            return Ok(result);
        }

        //[HttpGet]
        ////[Authorize]
        //public async Task<IActionResult> GetTopicById([FromQuery] int id)
        //{
        //    var result = await _topicService.GetTopicById(id);
        //    return Ok(result);
        //}



        //[HttpDelete]
        //public async Task<IActionResult> DeleteTopic([FromQuery] int id)
        //{
        //    var result = await _topicService.DeleteTopic(id);
        //    return Ok(result);
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetTopicByCourseId([FromQuery] int id)
        //{
        //    var result = await
        //}
    }
}
