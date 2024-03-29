﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Domains;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF;
using TAS.Data.Entities;
using TAS.Data.S3Object;
using TAS.Infrastructure.Helpers;

namespace TAS.Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<QuestionService> _logger;
        private readonly IS3StorageService _s3StorageService;

        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<QuestionService> logger, IS3StorageService s3StorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _s3StorageService = s3StorageService;
        }

        public async Task<bool> AddQuestion(CreateQuestionRequestDto request)
        {
            try
            {
                Question question = new Question();
                if (request != null)
                {
                    if (request.Image != null)
                    {
                        S3RequestData s3RequestData = new S3RequestData
                        {
                            BucketName = "tas",
                            InputStream = request.Image.OpenReadStream(),
                            Name = request.Image.FileName,
                        };
                        await _s3StorageService.UploadFileAsync(s3RequestData).ConfigureAwait(false);
                        question.Image = _s3StorageService.GetFileUrlDontExpires(s3RequestData);
                    }
                    List<QuestionAnswerDto> list = new List<QuestionAnswerDto>();
                    if (request.QuestionAnswers != null)
                    {
                        list = JsonConvert.DeserializeObject<List<QuestionAnswerDto>>(request.QuestionAnswers);
                    }
                    int partId = _unitOfWork.QuestionRepository.GetPartIdByTestId(request.TestId);
                    question.PartId = partId;
                    question.Description = request.Description;
                    var listquestionAnswer = _mapper.Map<List<QuestionAnswer>>(list);
                    question.QuestionAnswers = listquestionAnswer;
                    await _unitOfWork.QuestionRepository.AddAsync(question).ConfigureAwait(false);
                    await _unitOfWork.CommitAsync().ConfigureAwait(false);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
            throw new NotImplementedException();
        }

        public async Task<bool> AddQuestionForTest(CreateQuestionForTestRequestDto requestDto)
        {
            try
            {
                //var partId =  _unitOfWork.QuestionRepository.GetPartIdByTestId(requestDto.TestId);
                //Question question = new Question();
                //if (partId != 0)
                //{
                //    question.PartId = partId;
                //    question.Description = requestDto.Descrition;
                //    question.QuestionAnswers.Add(new QuestionAnswer()
                //    {
                //        ResultA = requestDto.AnswerA,
                //        ResultB = requestDto.AnswerB,
                //        ResultC = requestDto.AnswerC,
                //        ResultD = requestDto.AnswerD,
                //        CorrectResult = requestDto.CorrectAnswer
                //    });
                //}
                //await _unitOfWork.QuestionRepository.AddAsync(question);
                //await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
                        if (part.Url != null)
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
                            List<QuestionAnswer> questionAnswer = _unitOfWork.QuestionRepository.GetlistQuestionAnswerByQuesId(ques.QuestionId);
                            var quesmap = _mapper.Map<List<GetQuestionAnswerDto>>(questionAnswer);
                            question1.QuestionAnswers = quesmap;
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
                //foreach (var item in question)
                //{
                //    GetQuestionByTestIdResponseDto ques = new GetQuestionByTestIdResponseDto();
                //    ques.QuestionId = item.QuestionId;
                //    ques.Description = item.Description;
                //    ques.Image = item.Image;
                //    QuestionAnswer questionAnswer = questionAnswerById(item.QuestionId).Result;
                //    if (questionAnswer != null)
                //    {
                //        if (questionAnswer.ResultA != null)
                //        {
                //            ques.ResultA = questionAnswer.ResultA!;
                //        }
                //        if (questionAnswer.ResultB != null)
                //        {
                //            ques.ResultB = questionAnswer.ResultB!;
                //        }
                //        if (questionAnswer.ResultC != null)
                //        {
                //            ques.ResultC = questionAnswer.ResultC!;
                //        }
                //        if (questionAnswer.ResultD != null)
                //        {
                //            ques.ResultD = questionAnswer.ResultD!;
                //        }
                //        if (questionAnswer.CorrectResult != null)
                //        {
                //            ques.CorrectResult = questionAnswer.CorrectResult!;
                //        }
                //    }
                //    result.Add(ques);
                //}
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
                    var question = await _unitOfWork.QuestionRepository.GetQuestionById(request.QuestionId).FirstOrDefaultAsync().ConfigureAwait(false);
                    if (request.Image != null)
                    {
                        S3RequestData s3RequestData = new S3RequestData
                        {
                            BucketName = "tas",
                            InputStream = request.Image.OpenReadStream(),
                            Name = request.Image.FileName,
                        };
                        await _s3StorageService.UploadFileAsync(s3RequestData).ConfigureAwait(false);
                        question.Image = _s3StorageService.GetFileUrlDontExpires(s3RequestData);
                    }
                    question.Description = request.Description;
                    List<GetQuestionAnswerDto> answerDtos = JsonConvert.DeserializeObject<List<GetQuestionAnswerDto>>(request.QuestionAnswers);
                    var listquestionAnswer = _mapper.Map<List<QuestionAnswer>>(answerDtos);
                    foreach (var item in listquestionAnswer)
                    {
                        item.QuestionId = request.QuestionId;
                    }
                    _unitOfWork.QuestionRepository.UpdateQuestionAnswer(listquestionAnswer);
                    //question.QuestionAnswers = listquestionAnswer;
                    var result = _unitOfWork.CommitAsync().ConfigureAwait(false);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
                throw new Exception(e.Message);
            }
        }
    }
}