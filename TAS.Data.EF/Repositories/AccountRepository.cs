using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF.Repositories.Interfaces;
using TAS.Data.Entities;
using TAS.Infrastructure.Constants;

namespace TAS.Data.EF.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(TASContext context) : base(context)
        {

        }

        public IQueryable<Account> GetAll()
        {
            return _context.Set<Account>().Where(a => a.IsDeleted == Common.IsNotDelete);
        }

        public IQueryable<Account> GetAccountManagement()
		{
			return _context.Accounts.Include(a => a.Roles);

        }

		public IQueryable<Account> GetAllAccount()
        {
            return _context.Set<Account>().Where(a => a.IsDeleted.Equals(Common.IsNotDelete));
        }

        public Account Getuser(UserLoginRequestDto requestDto)
        {
            var user = _context.Accounts.Include(x => x.Roles).Where(x=>x.Username.Equals(requestDto.UserName) && x.IsDeleted==Common.IsNotDelete).FirstOrDefault();
            return user;

        }

        public Account GetAccountByIdReturnAcc(int id)
        {
            return _context.Accounts.Include(a => a.Roles).FirstOrDefault(x => x.AccountId == id);
        }
        public IQueryable<Account> GetAccountById(int accountId)
        {
            return _context.Set<Account>().Where(a => a.AccountId == accountId && a.IsDeleted == Common.IsNotDelete);
        }

        public IQueryable<Account> GetAllTeacher()
        {
            return _context.Accounts.Where(a => a.Roles.Any(r => r.RoleId == 3 ));
        }
        public IQueryable<Account> GetAllAccounts_Manage()
        {
            return _context.Accounts.Include(a => a.Roles);
        }

        public IQueryable<Enterprise> GetAllEnterprise()
        {
            //return _context.Accounts.Where(a => a.Roles.Any(r => r.RoleId == 4));
            return _context.Enterprises;
        }

        public string GetEnterpriseNameById(int id)
        {
            return _context.Enterprises.Where(e => e.AccountId == id).Select(e => e.ShortName).FirstOrDefault();
        }

        public Task<Account> GetUserByEmail(string email)
        {
            return _context.Accounts.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public Task<Account> GetUserByUsername(string username)
        {
            return _context.Accounts.FirstOrDefaultAsync(x => x.Username.Equals(username));
        }
    }
}
