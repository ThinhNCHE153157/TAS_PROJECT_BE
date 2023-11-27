using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.Entities;

namespace TAS.Application.Services.Interfaces
{
	public interface IAccountService
	{
        public Task<List<Account>> GetAccounts();
        public Task<List<AccountManageResponseDto>> GetAccountManagement();
		public Task<List<Account>> GetAllAccounts();
		public Task<bool> UserRegister(UserRegisterRequestDto request);
		public Task<Account> UserLogin(UserLoginRequestDto userLogin);
        public Task<Account> GetAccountByIdReturnAcc(int id);
        public Task<bool> AddUser(AccountAddRequestDto acc_request);
        public Task<bool> UpdateUser(AccountAddRequestDto user_request, int id);
        public Task<GetAccountByIdResponseDto> GetAccountById(int id);
        public Task<List<AccountManageResponseDto>> GetAccountInClass(int classId);
        public Task<List<AccountTeacherName>> GetAllTeacher();
    }
}
