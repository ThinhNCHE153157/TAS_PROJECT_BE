using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Responses;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories.Interfaces
{
    public interface IFlashcardRepository : IBaseRepository<Flashcard>
    {
        List<GetFlashcardByAccountIdResponseDto> GetFlashCardByAccountId(int accountId);
    }
}
