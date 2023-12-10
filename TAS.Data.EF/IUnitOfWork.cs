﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.EF.Repositories.Interfaces;

namespace TAS.Data.EF
{
    public interface IUnitOfWork
    {

        public IAccountRepository AccountRepository { get; }
        public ICourseRepository CourseRepository { get; }
        public IQuestionRepository QuestionRepository { get; }
        public ITestRepository TestRepository { get; }

        public IRoleRepository RoleRepositery { get; }
        public IClassRepository ClassRepository { get; }

        public Task CommitAsync();

        public void Commit();
    }
}
