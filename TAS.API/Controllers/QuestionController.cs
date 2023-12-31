﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;

namespace TAS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly ITestService _testService;

        public QuestionController(IQuestionService questionService, ITestService testService)
        {
            _questionService = questionService;
            _testService = testService;
        }

        [HttpGet]
        public async Task<IActionResult> getQuestionHomepage()
        {
            var result = await _questionService.getQuestionHomepage();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestion()
        {
            var result = await _questionService.GetAllQuestion();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionById([FromQuery] int id)
        {
            var result = await _questionService.GetQuestionById(id);
            return Ok(result);
        }
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetQuestionByTestId([FromQuery] int request)
        {
            var result = await _questionService.GetQuestionByTestId(request);
            var part = await _testService.GetPartByTestId(request).ConfigureAwait(false);
            var url = "";
            if (part != null)
            {
                url = part.FirstOrDefault().Url;
            }
            dynamic response = new
            {
                testId = request,
                url = url,                                                                                                                      
                questions = result
            };
            return Ok(response);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetQuestionByCourseId([FromQuery] int request)
        {
            var result = await _questionService.GetQuestionByCourseId(request);
            return Ok(result);
        }

        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> UpdateQuestion([FromBody] UpdateQuestionRequestDto request)
        {
            var result = await _questionService.UpdateQuestion(request);
            return Ok(result);
        }
        [HttpDelete]
        //[Authorize]
        public async Task<IActionResult> DeleteQuestion([FromQuery] int questionId)
        {
            var result = await _questionService.DeleteQuestion(questionId);
            return Ok(result);
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionRequestDto request)
        {
            if (request != null)
            {
                var result = await _questionService.AddQuestion(request);
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
