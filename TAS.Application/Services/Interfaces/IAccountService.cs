using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.Entities;

namespace TAS.Application.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<List<Account>> GetAccounts();
        public Task<Account> GetUserByEmail(string email);
        public Task<bool> updateOtp(string email, string otp, DateTime ExpriseTime);
        public Task<List<AccountManageResponseDto>> GetAccountManagement();
        public Task<List<Account>> GetAllAccounts();
        public Task<bool> UserRegister(UserRegisterRequestDto request);
        public Task<Account> UserLogin(UserLoginRequestDto userLogin);
        public Task<Account> GetAccountByIdReturnAcc(int id);
        public Task<List<AccountManageResponseDto>> GetAllAccounts_Manage();
        public Task<bool> AddUser(AccountAddRequestDto request);
        public Task<bool> UpdateUser(AccountAddRequestDto request, int id);
        public Task<GetAccountByIdResponseDto> GetAccountById(int id);
        public Task SendEmailAsync(MailRequestDto mailRequest);
        public Task<bool> ChangePassword(ChangePasswordRequestDto request);
        public Task<bool> UpdateAvatar(UpdateAvatarRequestDto request);
        public Task<bool> DeleteAvatar(int id);
        public Task<List<AccountTeacherName>> GetAllTeacher();
        public Task<List<Enterprise>> GetAllEnterprise();
        public string GetEnterpriseNameById(int id);
        public Task<bool> VerifyAccount(string id, string email);

    }
}
