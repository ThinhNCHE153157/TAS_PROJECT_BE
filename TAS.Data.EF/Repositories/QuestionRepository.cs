using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.EF.Repositories.Interfaces;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories
{
    public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(TASContext context) : base(context)
        {
        }

        public IQueryable<Question> GetAllQuestion()
        {
            return _context.Set<Question>();
        }

        public IQueryable<Question> GetQuestionById(int questionId)
        {
            return _context.Questions.Include(x => x.Test);
        }
    }
}
