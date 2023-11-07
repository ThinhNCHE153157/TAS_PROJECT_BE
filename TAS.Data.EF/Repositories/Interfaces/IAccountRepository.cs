using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories.Interfaces
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        public IQueryable<Account> GetAll();
        public Account Getuser(UserLoginRequestDto requestDto);
        public IQueryable<Account> GetAllAccount();
        public IQueryable<Account> GetAccountManagement();

        public IQueryable<Account> GetAllAccounts_Manage();
        public Account GetAccountById(int id);
    }
}
