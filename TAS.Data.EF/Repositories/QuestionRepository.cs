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

        public IQueryable<Question> GetQuestionByTestId(GetQuestionByTestIdRequestDto request)
        {
            return _context.Questions.Include(x => x.Part).ThenInclude(x => x.Test).Where(x => x.Part.TestId == request.TestId);
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
    }
}
