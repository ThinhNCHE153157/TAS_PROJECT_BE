using TAS.Data.Dtos.Responses;
using TAS.Data.Entities;

namespace TAS.Application.Services.Interfaces
{
    public interface IFlashcardService
    {
        public Task<List<GetFlashcardByAccountIdResponseDto>> GetFlashCardByAccountId(int accountId);
    }
}
