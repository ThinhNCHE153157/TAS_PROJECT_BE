using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.Entities;

namespace TAS.Application.Services.Interfaces
{
    public interface IFlashcardService
    {
        public Task<List<GetFlashcardByAccountIdResponseDto>> GetFlashCardByAccountId(int accountId);
        public Task<GetFlashcardByFlashcardResponseDto> GetFlashCardByFlashcardId(int flashid, int accountid);
        public Task<bool> CreateFlashCard(FlashCardRequestDto request);
        public Task<bool> UpdateFlashCard(FlashCardRequestDto request,int id);
        public Task<bool> DeleteFlashCard(int id);
        public Task<bool> AddItemcard(List<AddItemCardRequestDto> request);
        public Task<bool> AddFlashCardItem(AddFlashCardItemRequestDto request);
        public Task<bool> UpdateItemcard(UpdateItemRequestDto request);
        public Task<bool> DeleteItemcard(int id);
    }
}
