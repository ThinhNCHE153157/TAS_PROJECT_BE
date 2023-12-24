using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF;
using TAS.Data.Entities;
using TAS.Infrastructure.Helpers;

namespace TAS.Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<QuestionService> _logger;
        private readonly IMapper _mapper;

        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<QuestionService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddQuestion(CreateQuestionRequestDto request)
        {
            //try
            //{
            //    if (request != null)
            //    {
            //        var question =  _mapper.Map<Question>(request);
            //        var result =  _unitOfWork.QuestionRepository.CreateQuestion(question,question.QuestionNavigation);
            //        return result;
            //    }
            //    return false;
            //}catch(Exception e)
            //{
            //    _logger.LogError(e.Message);    
            //    return false;
            //}
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteQuestion(int questionId)
        {
            try
            {
                if (StringHelper.IsIntNumber(questionId.ToString()))
                {
                    var result = _unitOfWork.QuestionRepository.DeleteQuestion(questionId);
                    return result;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<QuestionDashboardResponseDto>> GetAllQuestion()
        {
            try
            {
                var listQuestion = await _unitOfWork.QuestionRepository.GetAllQuestion().ToListAsync().ConfigureAwait(false);
                var result = _mapper.Map<List<QuestionDashboardResponseDto>>(listQuestion);
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<GetQuestionByCourseIdResponseDto>> GetQuestionByCourseId(int courseId)
        {
            List<GetQuestionByCourseIdResponseDto> result = new List<GetQuestionByCourseIdResponseDto>();
            try
            {
                var listTopicId = _unitOfWork.CourseRepository.GetListTopicIdByCourseId(courseId).ToList();
                foreach (var item in listTopicId)
                {
                    var ListtestId = _unitOfWork.TestRepository.GetTestIdByTopicId(item);
                    foreach (var testid in ListtestId)
                    {
                        GetQuestionByCourseIdResponseDto question = new GetQuestionByCourseIdResponseDto();
                        question.TestId = testid;
                        var part = _unitOfWork.TestRepository.GetPartByTestId(testid).FirstOrDefault();
                        if (part != null)
                        {
                            question.Url = part.Url;
                        }
                        var listQuestion = await _unitOfWork.QuestionRepository.GetQuestionByTestId(testid).ToListAsync().ConfigureAwait(false);
                        foreach (var ques in listQuestion)
                        {
                            GetQuestionByTestIdResponseDto question1 = new GetQuestionByTestIdResponseDto();
                            question1.QuestionId = ques.QuestionId;
                            question1.Description = ques.Description;
                            question1.Image = ques.Image;
                            QuestionAnswer questionAnswer = questionAnswerById(ques.QuestionId).Result;
                            if (questionAnswer != null)
                            {
                                if (questionAnswer.ResultA != null)
                                {
                                    question1.ResultA = questionAnswer.ResultA!;
                                }
                                if (questionAnswer.ResultB != null)
                                {
                                    question1.ResultB = questionAnswer.ResultB!;
                                }
                                if (questionAnswer.ResultC != null)
                                {
                                    question1.ResultC = questionAnswer.ResultC!;
                                }
                                if (questionAnswer.ResultD != null)
                                {
                                    question1.ResultD = questionAnswer.ResultD!;
                                }
                                if (questionAnswer.CorrectResult != null)
                                {
                                    question1.CorrectResult = questionAnswer.CorrectResult!;
                                }
                            }
                            question.Questions.Add(question1);
                        }   
                    result.Add(question);
                    }
                }
                return result;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<GetQuestionByIdResponseDto> GetQuestionById(int id)
        {
            try
            {
                var question = await _unitOfWork.QuestionRepository.GetQuestionById(id).FirstOrDefaultAsync().ConfigureAwait(false);
                var result = _mapper.Map<GetQuestionByIdResponseDto>(question);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return null;
        }

        public async Task<List<GetQuestionByTestIdResponseDto>> GetQuestionByTestId(int request)
        {
            try
            {
                var question = await _unitOfWork.QuestionRepository.GetQuestionByTestId(request).ToListAsync().ConfigureAwait(false);
                var result = new List<GetQuestionByTestIdResponseDto>();
                foreach (var item in question)
                {
                    GetQuestionByTestIdResponseDto ques = new GetQuestionByTestIdResponseDto();
                    ques.QuestionId = item.QuestionId;
                    ques.Description = item.Description;
                    ques.Image = item.Image;
                    QuestionAnswer questionAnswer = questionAnswerById(item.QuestionId).Result;
                    if (questionAnswer != null)
                    {
                        if (questionAnswer.ResultA != null)
                        {
                            ques.ResultA = questionAnswer.ResultA!;
                        }
                        if (questionAnswer.ResultB != null)
                        {
                            ques.ResultB = questionAnswer.ResultB!;
                        }
                        if (questionAnswer.ResultC != null)
                        {
                            ques.ResultC = questionAnswer.ResultC!;
                        }
                        if (questionAnswer.ResultD != null)
                        {
                            ques.ResultD = questionAnswer.ResultD!;
                        }
                        if (questionAnswer.CorrectResult != null)
                        {
                            ques.CorrectResult = questionAnswer.CorrectResult!;
                        }
                    }
                    result.Add(ques);
                }
                return result;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<QuestionHomepageResponeDto>> getQuestionHomepage()
        {
            try
            {
                var listQuestion = await _unitOfWork.QuestionRepository.GetAllQuestion().ToListAsync().ConfigureAwait(false);
                var result = _mapper.Map<List<QuestionHomepageResponeDto>>(listQuestion);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<QuestionAnswer> questionAnswerById(int id)
        {
            try
            {
                var result = (QuestionAnswer)_unitOfWork.QuestionRepository.GetQuestionAnswerByQuesId(id);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> UpdateQuestion(UpdateQuestionRequestDto request)
        {
            try
            {
                if (request != null)
                {
                    var result = _unitOfWork.QuestionRepository.UpdateQuestion(request);
                    return result;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return false;
        }
    }
}
