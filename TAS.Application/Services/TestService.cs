using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF;
using TAS.Data.Entities;

namespace TAS.Application.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly ILogger<TestService> _logger;
        public readonly IQuestionService _questionService;
        public TestService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TestService> logger, IQuestionService questionService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _questionService = questionService;
        }

        public async Task<CourseResultResponseDto> CourseResult(int id)
        {
            try
            {
                var test = await _unitOfWork.TestRepository.GetTestResultById(id).FirstOrDefaultAsync().ConfigureAwait(false);
                if (test != null)
                {
                    var result = _mapper.Map<CourseResultResponseDto>(test);
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<GetTestByIdResponseDto> GetTestById(int id)
        {
            try
            {
                var test = await _unitOfWork.TestRepository.GetTestById(id).Include(x => x.Parts).ThenInclude(x => x.Questions).FirstOrDefaultAsync().ConfigureAwait(false);
                if (test != null)
                {
                    var result = _mapper.Map<GetTestByIdResponseDto>(test);
                    foreach (var item in result.Parts)
                    {
                        foreach (var ques in item.Questions)
                        {
                            var questionAnswer = _questionService.questionAnswerById(ques.QuestionId).Result;
                            if (questionAnswer != null)
                            {
                                if (questionAnswer.ResultA != null)
                                {
                                    ques.Answers.Add(questionAnswer.ResultA!);
                                }
                                if (questionAnswer.ResultB != null)
                                {
                                    ques.Answers.Add(questionAnswer.ResultB!);
                                }
                                if (questionAnswer.ResultC != null)
                                {
                                    ques.Answers.Add(questionAnswer.ResultC!);
                                }
                                if (questionAnswer.ResultD != null)
                                {
                                    ques.Answers.Add(questionAnswer.ResultD!);
                                }
                                if (questionAnswer.CorrectResult != null)
                                {
                                    ques.CorrectAnswer = questionAnswer.CorrectResult!;
                                }
                            }   
                        }
                    }
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<bool> UpdateTest(UpdateTestRequestDto request)
        {
            try
            {
                var test = _unitOfWork.TestRepository.GetTestById(request.TestId).FirstOrDefault();
                if (test != null)
                {

                    test.TestName = request.TestName;
                    test.TestTotalScore = request.TestTotalScore;
                    test.TestDuration = request.TestDuration;
                    test.TestDescription = request.TestDescription;
                    var result = _unitOfWork.TestRepository.UpdateTest(test);
                    return result;
                }
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateStatusTest(int id)
        {
            try
            {
                var result = _unitOfWork.TestRepository.UpdateStatusTest(id);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> CreateTestForCourse(CreateTestForCourseRequestDto request)
        {
            //try
            //{
            //    var test = _mapper.Map<Test>(request.Tests);
            //    var result = _unitOfWork.TestRepository.CreateTestForCourse(request.CourseId,test);
            //    return result;
            //}catch(Exception e)
            //{
            //    _logger.LogError(e.Message);
            //    return false;
            //}
            throw new NotImplementedException();
        }

        public async Task<List<GetListTestFreeResponseDto>> getListTestFreeResponseDtos()
        {
            try
            {
                var test = await _unitOfWork.TestRepository.GetListTestFree().Include(x => x.Parts).ToListAsync().ConfigureAwait(false);
                if (test != null)
                {
                    var result = _mapper.Map<List<GetListTestFreeResponseDto>>(test);

                    foreach (var item in result)
                    {
                        if (item != null)
                        {
                            List<int> listPart = _unitOfWork.TestRepository.GetPartsByTestId(item.TestId).ToList();
                            item.TotalPart = listPart.Count;
                            item.TestTotalQuestion = 0;
                            if (item.TotalPart != 0)
                            {
                                foreach (var part in listPart)
                                {
                                    var listQuestion = _unitOfWork.QuestionRepository.GetQuestionByPartId(part).ToList();
                                    item.TestTotalQuestion += listQuestion.Count;
                                }
                            }
                        }
                    }

                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<GetListPartOfTestResponseDto> getListPartOfTest(int testId)
        {
            GetListPartOfTestResponseDto result = new GetListPartOfTestResponseDto();
            try
            {
                var partAu = _unitOfWork.TestRepository.GetPartsAudioByTestId(testId).ToList();
                var partRe = _unitOfWork.TestRepository.GetPartsReadByTestId(testId).ToList();
                if (partAu != null)
                {
                    foreach (var item in partAu)
                    {
                        var listQuestion = _unitOfWork.QuestionRepository.GetQuestionByPartId(item).ToList();
                        result.PartAudio.Add(new PartOfTestResponseDto(item, listQuestion.Count));
                    }
                }
                if (partRe != null)
                {
                    foreach (var item in partRe)
                    {
                        var listQuestion = _unitOfWork.QuestionRepository.GetQuestionByPartId(item).ToList();
                        result.PartReading.Add(new PartOfTestResponseDto(item, listQuestion.Count));
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public async Task<List<GetAllTestResponseDto>> GetAllTest()
        {
            try
            {
                var result = _unitOfWork.TestRepository.GetAllTest().ToList();
                if (result != null)
                {
                    var response = _mapper.Map<List<GetAllTestResponseDto>>(result);
                    return response;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
    }
}
