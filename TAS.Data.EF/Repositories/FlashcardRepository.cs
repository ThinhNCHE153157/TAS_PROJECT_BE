using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF.Repositories.Interfaces;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories
{
    public class FlashcardRepository : BaseRepository<Flashcard>, IFlashcardRepository
    {
        private readonly IHttpContextAccessor _accessor;
        public FlashcardRepository(TASContext context, IHttpContextAccessor accessor) : base(context)
        {
            _accessor = accessor;
        }

        public bool AddFlashCardItem(ItemCard itemcard)
        {
            if (itemcard != null)
            {
                _context.ItemCards.Add(itemcard);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddItemcard(List<ItemCard> itemcard)
        {
            if (itemcard != null)
            {
                _context.ItemCards.AddRange(itemcard);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool CreateFlashCard(Flashcard flashcard)
        {
            try
            {
                _context.Flashcards.Add(flashcard);
                _context.SaveChanges();
                var httpContext = _accessor?.HttpContext;
                const string nameKey = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
                var currentUser = httpContext.User.Claims
                    .FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name))?.Value;
                AccountFlashcard accountFlashcard = new AccountFlashcard();
                var account = _context.Accounts.Where(x => x.Username == currentUser).FirstOrDefault();
                var flashcard1 = _context.Flashcards.Where(x => x.CreateUser == currentUser).FirstOrDefault();
                accountFlashcard.AccountId = account.AccountId;
                accountFlashcard.FlashcardId = flashcard1.FlashcardId;
                accountFlashcard.IsOwn = true;
                _context.AccountFlashcards.Add(accountFlashcard);
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteFlashCard(int id)
        {
            try
            {
                var x = _context.Flashcards.Where(x => x.FlashcardId == id).FirstOrDefault();
                var y = _context.AccountFlashcards.Where(x => x.FlashcardId == id).FirstOrDefault();
                if (y != null)
                {
                    _context.Remove(y);
                    _context.SaveChanges();
                    if (x != null)
                    {
                        _context.Remove(x);
                        _context.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<AccountFlashcard> GetAccountFlashcardsByAccountId(int accountId)
        {
            return _context.AccountFlashcards.Where(x => x.AccountId == accountId).ToList();
        }

        public List<Flashcard> GetFlashCardByAccountId(int accountId)
        {
            try
            {
                List<Flashcard> result = new List<Flashcard>();
                List<AccountFlashcard> listFlashcardId = _context.AccountFlashcards.Where(x => x.AccountId == accountId).ToList();
                if (listFlashcardId.Count != 0)
                {
                    foreach (var item in listFlashcardId)
                    {
                        Flashcard result1 = _context.Flashcards.Include(x => x.ItemCards).Where(x => x.FlashcardId == item.FlashcardId).FirstOrDefault();
                        result.Add(result1);

                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ItemCard GetItemCardById(int id)
        {
            return _context.ItemCards.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool UpdateFlashCard(FlashCardRequestDto request, int id)
        {
            try
            {
                var x = _context.Flashcards.Where(x => x.FlashcardId == id).FirstOrDefault();
                if (x != null)
                {
                    x.FlashcardName = request.FlashcardName;
                    x.Description = request.Description;
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
