using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Requests;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories.Interfaces
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        public IQueryable<Question> GetAllQuestion();
        public IQueryable<Question> GetQuestionById(int questionId);
        public IQueryable<Question> GetQuestionByTestId(GetQuestionByTestIdRequestDto request);
    }
}
