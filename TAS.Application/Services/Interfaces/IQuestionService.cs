using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.Entities;

namespace TAS.Application.Services.Interfaces
{
    public interface IQuestionService
    {
        public Task<List<QuestionHomepageResponeDto>> getQuestionHomepage();
        public Task<List<QuestionDashboardResponseDto>> GetAllQuestion();
        public Task<GetQuestionByIdResponseDto> GetQuestionById(int id);
        public Task<List<GetQuestionByTestIdResponseDto>> GetQuestionByTestId(GetQuestionByTestIdRequestDto request);
        //public Task<Question> CreateQuestion(CreateQuestionRequestDto request);
        public Task<bool> UpdateQuestion(UpdateQuestionRequestDto request);
        public Task<bool> DeleteQuestion(int questionId);
    }
}
