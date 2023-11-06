using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF;
using TAS.Data.Entities;
using TAS.Infrastructure.Constants;
using TAS.Infrastructure.Helpers;

namespace TAS.Application.Services
{ 
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AccountHomepageResponeDTO>> GetAccountManagement()
        {
            try
            {
				var listAccount = await _unitOfWork.AccountRepository.GetAccountManagement().ToListAsync().ConfigureAwait(false);
				var result = _mapper.Map<List<AccountHomepageResponeDTO>>(listAccount).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

		public async Task<List<Account>> GetAllAccounts()
		{
			try
			{
				var listAccount = await _unitOfWork.AccountRepository.GetAllAccount().ToListAsync().ConfigureAwait(false);
				return listAccount;
			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<bool> UserRegister(UserRegisterRequestDto request)
        {
            try
            {
                var user = _mapper.Map<Account>(request);
                user.Password = HashingHelper.EncryptPassword(request.Password);
                await _unitOfWork.AccountRepository.AddAsync(user).ConfigureAwait(false);
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
            }
            return false;
        }

        public async Task<Account> UserLogin(UserLoginRequestDto userLogin)
        {
            try
            {
                var user =  _unitOfWork.AccountRepository.Getuser(userLogin);
                if (user is not null && HashingHelper.VerifyPassword(userLogin.Password,user.Password))
                {
                    return user;
                }
            }catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<List<Account>> GetAccounts()
        {
            try
            {
                var result = await _unitOfWork.AccountRepository.GetAll().ToListAsync().ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

		public async Task<GetAccountByIdResponseDto> GetAccountById(int id)
		{
			try
			{
				var account = await _unitOfWork.AccountRepository.GetAccountById(id)
					.Where(x => x.IsDeleted == Common.IsNotDelete)
					.FirstOrDefaultAsync().ConfigureAwait(false);

				if (account != null)
				{
					var result = _mapper.Map<GetAccountByIdResponseDto>(account);
					return result;
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

			return null;
		}

	}
}
