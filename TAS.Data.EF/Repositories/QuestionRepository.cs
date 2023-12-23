using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Requests;
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
            return _context.Questions.Where(x => x.QuestionId == questionId);
        }

        public IQueryable<Question> GetQuestionByPartId(int id)
        {
            return _context.Questions.Include(x => x.Part).Where(x => x.Part.TestId == id);
        }

        public bool UpdateQuestion(UpdateQuestionRequestDto request)
        {
            var question = GetQuestionById(request.QuestionId).FirstOrDefault();
            var questionAnswer = _context.QuestionAnswers.Where(x => x.QuestionId == request.QuestionId).FirstOrDefault();
            if (question != null && questionAnswer!=null)
            {
                question.Description = request.Description;
                question.Image = request.Image;
                question.Type = request.Type;
                question.Note = request.Note;
                questionAnswer.ResultA = request.QuestionNavigation.ResultA;
                questionAnswer.ResultB = request.QuestionNavigation.ResultB;
                questionAnswer.ResultC = request.QuestionNavigation.ResultC;
                questionAnswer.ResultD = request.QuestionNavigation.ResultD;
                questionAnswer.CorrectResult = request.QuestionNavigation.CorrectResult;
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteQuestion(int questionId)
        {
            var question = GetQuestionById(questionId).FirstOrDefault();
            var questionAnswer = _context.QuestionAnswers.Where(x => x.QuestionId == questionId).FirstOrDefault();
            if (question != null && questionAnswer != null)
            {
                _context.Questions.Remove(question);
                _context.QuestionAnswers.Remove(questionAnswer);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool CreateQuestion(Question question, QuestionAnswer questionAnswer)
        {
            try
            {
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public QuestionAnswer GetQuestionAnswerByQuesId(int id)
        {
            try
            {
                return _context.QuestionAnswers.Where(x => x.QuestionId == id).FirstOrDefault()!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool AddQuestionResult(QuestionResult questionResult)
        {
            try
            {
                _context.QuestionResults.Add(questionResult);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<QuestionResult> questionResults(int testId, int accountId)
        {
            int testResultId = _context.TestResults.Where(x => x.TestId == testId && x.AccountId == accountId).OrderByDescending(x=>x.TestResultId).FirstOrDefault().TestResultId;
            if (testResultId != null)
            {
                return _context.QuestionResults.Where(x => x.TestResultId == testResultId);
            }
            else
            {
                return null;
            }
        }
    }
}
