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
        public IQueryable<Question> GetQuestionByPartId(int id);
        public IQueryable<Question> GetQuestionByTestId(int id);
        public IQueryable<Question> GetQuestionByCourseId(int id);
        public QuestionAnswer GetQuestionAnswerByQuesId(int id);
        public bool CreateQuestion(Question question, QuestionAnswer questionAnswer);
        public bool UpdateQuestion(UpdateQuestionRequestDto request);
        public bool DeleteQuestion(int questionId);
        public bool AddQuestionResult(QuestionResult questionResult);
        public IEnumerable<QuestionResult> questionResults(int testId, int accountId);
    }
}
