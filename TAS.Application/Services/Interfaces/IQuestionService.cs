using TAS.Data.Dtos.Responses;
using TAS.Data.Entities;

namespace TAS.Application.Services.Interfaces
{
    public interface IQuestionService
    {
        public Task<List<QuestionHomepageResponeDto>> getQuestionHomepage();
        public Task<List<QuestionDashboardResponseDto>> GetAllQuestion();
        public Task<GetQuestionByIdResponseDto> GetQuestionById(int id);
    }
}
