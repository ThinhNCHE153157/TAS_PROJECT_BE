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
        public Task<Account> GetUserByEmail(string email);
        public Task<Account> GetUserByUsername(string username);
        public Account Getuser(UserLoginRequestDto requestDto);
        public IQueryable<Account> GetAllAccount();
        public IQueryable<Account> GetAccountManagement();
        public IQueryable<Account> GetAccountById(int AccountId);
        public Account GetAccountByIdReturnAcc(int id);
        public IQueryable<Account> GetAccountInClass(int classId);
        public IQueryable<Account> GetAllTeacher();
        public IQueryable<Account> GetAllAccounts_Manage();
        public IQueryable<Enterprise> GetAllEnterprise();
        public string GetEnterpriseNameById(int id);
    }
}
