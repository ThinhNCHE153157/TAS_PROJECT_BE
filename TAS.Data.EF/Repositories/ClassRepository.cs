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
    public class ClassRepository : BaseRepository<Class>, IClassRepository
    {
        public ClassRepository(TASContext context) : base(context) 
        {

        }

        public IQueryable<Class> GetClassCode()
        {
            return _context.Classes.Select(c => new Class
            {
                ClassId = c.ClassId,
                ClassCode = c.ClassCode,
            }).AsQueryable();
        }

        public IQueryable<Class> GetAllClassesManage()
        {
            return _context.Classes
                .Include(c => c.Accounts)
                    .ThenInclude(a => a.Roles)
                .Where(c => c.Accounts.Any(a => a.Roles.Any(r => r.RoleId == 3)))
                .AsQueryable();
        }

        //public IQueryable<Class> GetAllClassesManage()
        //{
        //    return _context.Classes
        //        .Where(c => c.Accounts.Any(a => a.Roles.Any(r => r.RoleId == 3)))
        //        .Select(c => new Class
        //        {
        //            ClassId = c.ClassId,
        //            ClassName = c.ClassName,
        //            // ... Chọn các trường khác nếu cần
        //            Accounts = c.Accounts
        //                .Where(a => a.Roles.Any(r => r.RoleId == 3))
        //                .Select(a => new Account
        //                {
        //                    AccountId = a.AccountId,
        //                    Username = a.Username,
        //                    // ... Chọn các trường khác nếu cần
        //                    Roles = a.Roles.Where(r => r.RoleId == 3).ToList()
        //                })
        //                .ToList()
        //        })
        //        .AsQueryable();
        //}

        public IQueryable<Class> GetClassByStudentId(int studentId)
        {
            var result = _context.Classes
                .Where(c => c.Accounts.Any(a => a.AccountId == studentId))
                .AsQueryable();

            return result;
        }

        public Class GetClassDetailById(int id)
        {
            return _context.Classes.Include(c => c.Accounts).ThenInclude(a => a.Roles).SingleOrDefault(cl => cl.ClassId == id);
        }

        public Class GetClassByClassCode(string classCode)
        {
            return _context.Classes.FirstOrDefault(c => c.ClassCode.ToLower().Equals(classCode.ToLower()));
        }
    }
}
