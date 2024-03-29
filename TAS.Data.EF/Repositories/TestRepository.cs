﻿using Microsoft.EntityFrameworkCore;
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
    public class TestRepository : BaseRepository<Test>, ITestRepository
    {
        public TestRepository(TASContext context) : base(context)
        {
        }


        public bool UpdateStatusTest(int testId)
        {
            var find = _context.Set<Test>().Where(x => x.TestId == testId).FirstOrDefault();
            if (find != null)
            {
                if (find.IsDeleted == true)
                {
                    find.IsDeleted = false;
                }
                else
                {
                    find.IsDeleted = true;
                }
                _context.Update(find);
                int n = _context.SaveChanges();
                if (n > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }


        public IQueryable<Test> GetTestById(int testId)
        {
            return _context.Set<Test>().Where(x => x.TestId == testId);
        }

        public IQueryable<Test> GetTestResultById(int testId)
        {
            return _context.Set<Test>().Include(x => x.TestResults).Where(x => x.TestId == testId);
        }

        public bool UpdateTest(Test test)
        {
            _context.Update(test);
            int n = _context.SaveChanges();
            if (n > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<Test> GetListTestFree()
        {
            return _context.Set<Test>().Where(x => x.IsDeleted == false && x.TopicId==null);
        }

        public int TotalPartOfTest(int testId)
        {
            return _context.Set<Part>().Where(x => x.TestId == testId).Count();
        }

        public IEnumerable<int> GetPartsByTestId(int testId)
        {
            return _context.Set<Part>().Where(x => x.TestId == testId).Select(x => x.PartId);
        }
        public IEnumerable<int> GetPartsAudioByTestId(int testId)
        {
            return _context.Set<Part>().Where(x => x.TestId == testId && x.Type==false).Select(x => x.PartId);
        }
        public IEnumerable<int> GetPartsReadByTestId(int testId)
        {
            return _context.Set<Part>().Where(x => x.TestId == testId && x.Type==true).Select(x => x.PartId);
        }

        public IEnumerable<Test> GetAllTest()
        {
            return _context.Set<Test>();
        }

        public bool SaveTestResult(TestResult request)
        {
            try
            {
                _context.Set<TestResult>().Add(request);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public int GetTestResultId(int testId, int accountId)
        {
            try
            {
                return _context.Set<TestResult>().Where(x => x.TestId == testId && x.AccountId == accountId).OrderByDescending(x=>x.TestResultId).FirstOrDefault().TestResultId;
            }
            catch
            (Exception e)
            {
                return 0;
            }
        }

        public TestResult GetTestResult(int testResultId)
        {
            try
            {
                return _context.Set<TestResult>().Where(x => x.TestResultId == testResultId).FirstOrDefault();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public int GetPartIdByTopicId(int topicId)
        {
            var result = _context.Set<Test>().Where(x => x.TopicId == topicId).FirstOrDefault();
            if (result != null)
            {
                return _context.Set<Part>().Where(x => x.TestId == result.TestId).FirstOrDefault().PartId;
            }
            return 0;
        }

        public List<Part> GetPartByTestId(int testId)
        {
            return _context.Set<Part>().Where(x => x.TestId == testId).ToList();
        }

        public List<int> GetTestIdByTopicId(int topicId)
        {
            return _context.Set<Test>().Where(x => x.TopicId == topicId).Select(x => x.TestId).ToList();
        }

        public bool AddPart(Part part)
        {
            _context.Set<Part>().Add(part);
            int n = _context.SaveChanges();
            if (n > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<TestResult> GetTestResultByAccountd(int accountId)
        {
            return _context.TestResults.Where(x => x.AccountId == accountId).ToList();
        }

        public bool DeleteTestByTestId(int testId)
        {
            var result = _context.Set<Test>().Where(x => x.TestId == testId).FirstOrDefault();
            if (result != null)
            {
                result.IsDeleted = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
