using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;
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
    }
}
