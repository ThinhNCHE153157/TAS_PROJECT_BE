using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Requests;
using TAS.Data.EF.Repositories.Interfaces;
using TAS.Data.Entities;
using TAS.Infrastructure.Constants;

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
            return _context.Questions.Where(x => x.QuestionId == questionId & x.IsDeleted==Common.IsNotDelete);
        }

        public IQueryable<Question> GetQuestionByPartId(int id)
        {
            return _context.Questions.Include(x => x.Part).Where(x => x.Part.TestId == id && x.IsDeleted==Common.IsNotDelete);
        }

        public bool UpdateQuestion(UpdateQuestionRequestDto request)
        {
            //var question = GetQuestionById(request.QuestionId).FirstOrDefault();
            //var questionAnswer = _context.QuestionAnswers.Where(x => x.QuestionId == request.QuestionId).FirstOrDefault();
            //if (question != null && questionAnswer != null)
            //{
            //    question.Description = request.Description;
            //    question.Image = request.Image;
            //    question.Type = request.Type;
            //    question.Note = request.Note;
            //    questionAnswer.ResultA = request.QuestionNavigation.ResultA;
            //    questionAnswer.ResultB = request.QuestionNavigation.ResultB;
            //    questionAnswer.ResultC = request.QuestionNavigation.ResultC;
            //    questionAnswer.ResultD = request.QuestionNavigation.ResultD;
            //    questionAnswer.CorrectResult = request.QuestionNavigation.CorrectResult;
            //    _context.SaveChanges();
            //    return true;
            //}
            return false;
        }

        public bool DeleteQuestion(int questionId)
        {
            var question = GetQuestionById(questionId).FirstOrDefault();
            //var questionAnswer = _context.QuestionAnswers.Where(x => x.QuestionId == questionId).FirstOrDefault();
            if (question != null )
            {
                question.IsDeleted = true;
                //_context.Questions.Remove(question);
                //_context.QuestionAnswers.Remove(questionAnswer);
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
            int testResultId = _context.TestResults.Where(x => x.TestId == testId && x.AccountId == accountId).OrderByDescending(x => x.TestResultId).FirstOrDefault().TestResultId;
            if (testResultId != null)
            {
                return _context.QuestionResults.Where(x => x.TestResultId == testResultId);
            }
            else
            {
                return null;
            }
        }

        public IQueryable<Question> GetQuestionByTestId(int id)
        {
            try
            {
                var partId = _context.Parts.Where(x => x.TestId == id).Select(x => x.PartId).FirstOrDefault();
                return _context.Questions.Include(x => x.QuestionAnswers).Where(x => x.PartId == partId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IQueryable<Question> GetQuestionByCourseId(int id)
        {
            try
            {
                var topicList = _context.Topics.Where(x => x.CourseId == id).Select(x => x.TopicId).ToList();
                if (topicList != null || topicList.Count != 0)
                {
                    var result = _context.Tests.Include(x => x.Parts).ThenInclude(x => x.Questions).ThenInclude(x => x.QuestionAnswers).Where(x => topicList.Contains((int)x.TopicId!)).SelectMany(x => x.Parts).SelectMany(x => x.Questions).Include(x => x.QuestionAnswers);
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int GetPartIdByTestId(int testId)
        {
            return _context.Set<Part>().Where(x => x.TestId == testId).FirstOrDefault().PartId;
        }

        public List<QuestionAnswer> GetlistQuestionAnswerByQuesId(int id)
        {
            try
            {
                return _context.QuestionAnswers.Where(x => x.QuestionId == id).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
