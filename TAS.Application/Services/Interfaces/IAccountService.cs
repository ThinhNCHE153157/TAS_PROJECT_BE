using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.Entities;

namespace TAS.Application.Services.Interfaces
{
	public interface IAccountService
	{
        public Task<List<Account>> GetAccounts();
        public Task<List<AccountHomepageResponeDTO>> GetAccountManagement();
		public Task<List<Account>> GetAllAccounts();
		public Task<bool> UserRegister(UserRegisterRequestDto request);
		public Task<Account> UserLogin(UserLoginRequestDto userLogin);
	}
}
