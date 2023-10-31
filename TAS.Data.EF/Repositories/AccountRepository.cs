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
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(TASContext context) : base(context)
        {

        }

		public IQueryable<Account> GetAccountManagement()
		{
			return _context.Set<Account>().Where(a => a.IsDeleted.Equals(Common.IsNotDelete));

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
    }
}
