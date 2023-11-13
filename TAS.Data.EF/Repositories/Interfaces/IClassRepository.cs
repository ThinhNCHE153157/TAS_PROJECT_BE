using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories.Interfaces
{
    public interface IClassRepository : IBaseRepository<Class>
    {
        public IQueryable<Class> GetAllClasses();
        public Class GetClassDetailById(int id);

        public IQueryable<Class> GetClassByStudentId(int studentId);
        public IQueryable<Class> GetClassCode();
        public Class GetClassByClassCode(string classCode);

    }
}
