using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return _context.Set<Account>().Where(a => a.IsDeleted.Equals(Common.IsNotDelete));
        }
    }
}
