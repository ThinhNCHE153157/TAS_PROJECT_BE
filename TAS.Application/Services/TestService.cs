using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Domains;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF;
using TAS.Data.Entities;
using static System.Net.Mime.MediaTypeNames;
using TAS.Data.S3Object;

namespace TAS.Application.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly ILogger<TestService> _logger;
        public readonly IQuestionService _questionService;
        public readonly IS3StorageService _s3StorageService;
        public TestService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<TestService> logger, IQuestionService questionService, IS3StorageService s3StorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _questionService = questionService;
            _s3StorageService = s3StorageService;
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
            try
            {
                string url = "";
                if (request.Url != null)
                {
                    S3RequestData s3RequestData = new S3RequestData
                    {
                        BucketName = "tas",
                        InputStream = request.Url.OpenReadStream(),
                        Name = request.Url.FileName,
                    };
                    await _s3StorageService.UploadFileAsync(s3RequestData).ConfigureAwait(false);
                    url = _s3StorageService.GetFileUrl(s3RequestData);
                }

                Test test = new Test();
                test.TestName = request.TestName;
                test.TopicId = request.TopicId;
                await _unitOfWork.TestRepository.AddAsync(test).ConfigureAwait(false);
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
                Part part = new Part();
                part.TestId = test.TestId;
                part.Url = url;
                part.Type = (request.Type == 1) ?true:false;
                var result = _unitOfWork.TestRepository.AddPart(part);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
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

        public async Task<bool> SaveTestResult(SaveTestResultRequestDto request)
        {
            try
            {

                var testResult = _mapper.Map<TestResult>(request);
                var result = _unitOfWork.TestRepository.SaveTestResult(testResult);
                var testResultId = _unitOfWork.TestRepository.GetTestResultId(request.TestId, request.AccountId);
                foreach (var item in request.ListAnswer)
                {
                    QuestionResultDto questionResult = new QuestionResultDto(testResultId,request.NumberCorrect, item.QuestionId.ToString(), item.UserAnswer);
                    var answer = _mapper.Map<QuestionResult>(questionResult);
                    _unitOfWork.QuestionRepository.AddQuestionResult(answer);
                }
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<SaveTestResultResponseDto> TestDetail(int testId, int accountId)
        {
            SaveTestResultResponseDto response = new SaveTestResultResponseDto();
            var useranswer = _unitOfWork.QuestionRepository.questionResults(testId, accountId);
            foreach (var item in useranswer)
            {
                var userAnswer = _mapper.Map<UserAnswerDto>(item);
                response.userAnswers.Add(userAnswer);
                response.NumCorrect= item.Description;
            }
            var test = await _unitOfWork.TestRepository.GetTestById(testId).Include(x => x.Parts).ThenInclude(x => x.Questions).FirstOrDefaultAsync().ConfigureAwait(false);
            if (test != null)
            {
                response.TestName = test.TestName;
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
                foreach (var item in result.Parts)
                {
                    foreach (var ques in item.Questions)
                    {
                        response.questionDtos.Add(ques);
                    }
                }
            }
            return response;
        }

        public int GetPartIdByTopicId(int topicId)
        {
            try
            {
                if (topicId != 0)
                {
                    var result = _unitOfWork.TestRepository.GetPartIdByTopicId(topicId);
                    return result;
                }
                return 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return 0;
            }
        }

        public async Task<List<Part>> GetPartByTestId(int testId)
        {
            try
            {
                var result = _unitOfWork.TestRepository.GetPartByTestId(testId);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
           
        }

        public async Task<List<TestResult>> GetTestResult(int accountId)
        {
            try
            {
                var result = _unitOfWork.TestRepository.GetTestResultByAccountd(accountId).ToList();
                return result;
            }
            catch
            {
                _logger.LogError("GetTestResult error"); 
                return null;
            }
        }
    }
}
