using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF.Repositories.Interfaces;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories
{
    public class FlashcardRepository : BaseRepository<Flashcard>, IFlashcardRepository
    {
        public FlashcardRepository(TASContext context) : base(context)
        {
        }

        public List<GetFlashcardByAccountIdResponseDto> GetFlashCardByAccountId(int accountId)
        {
            try
            {
                List<GetFlashcardByAccountIdResponseDto> result = new List<GetFlashcardByAccountIdResponseDto>();
                List<AccountFlashcard> listFlashcardId = _context.AccountFlashcards.Where(x => x.AccountId == accountId).ToList();
                if (listFlashcardId.Count != 0)
                {
                    foreach (var item in listFlashcardId)
                    {
                        Flashcard result1 = _context.Flashcards.Include(x=>x.ItemCards).Where(x => x.FlashcardId == item.FlashcardId).FirstOrDefault();
                        GetFlashcardByAccountIdResponseDto flashcard = new GetFlashcardByAccountIdResponseDto();
                        flashcard.FlashcardId = item.FlashcardId;
                        flashcard.AccountId = item.AccountId;
                        if (result1 != null)
                        {
                            flashcard.FlashcardName = result1.FlashcardName;
                            flashcard.Description = result1.Description;
                            flashcard.CreateUser = result1.CreateUser;
                            flashcard.NumberOfItem = result1.ItemCards.Count;
                            flashcard.IsOwn = item.IsOwn;
                            result.Add(flashcard);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
