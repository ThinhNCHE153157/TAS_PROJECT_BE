using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Data;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF;
using TAS.Data.Entities;
using TAS.Data.S3Object;
using TAS.Infrastructure.Constants;
using TAS.Infrastructure.Helpers;
using static TAS.Infrastructure.Enums.SystemEnum;

namespace TAS.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public readonly ILogger<AccountService> _logger;
        public readonly IS3StorageService _s3StorageService;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IS3StorageService s3StorageService, ILogger<AccountService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _s3StorageService = s3StorageService;
            _logger = logger;
        }

        public async Task<List<AccountManageResponseDto>> GetAccountManagement()
        {
            try
            {
                var accounts = await _unitOfWork.AccountRepository.GetAccountManagement().ToListAsync().ConfigureAwait(false);
                var result = _mapper.Map<List<AccountManageResponseDto>>(accounts);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UserRegister(UserRegisterRequestDto request)
        {
            try
            {
                var user = _mapper.Map<Account>(request);
                user.Password = HashingHelper.EncryptPassword(request.Password);
                user.Roles.Add(_unitOfWork.RoleRepositery.GetRoleById(4));
                user.IsVerified = false;
                await _unitOfWork.AccountRepository.AddAsync(user).ConfigureAwait(false);
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return false;
        }

        public async Task<bool> VerifyAccount(string id,string email)
        {
            try
            {
                Account account = await _unitOfWork.AccountRepository.GetUserByEmail(email).ConfigureAwait(false);
                if (account != null)
                {
                    if (account.Otp == id)
                    {
                        if (account.Otpexpiretime < System.DateTime.Now)
                        {
                            account.IsVerified = true;
                            account.Otp = null;
                            account.Otpexpiretime = null;
                            _unitOfWork.Commit();
                            return true;
                        }
                    }
                }
                        return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<Account> UserLogin(UserLoginRequestDto userLogin)
        {
            try
            {
                var user = _unitOfWork.AccountRepository.Getuser(userLogin);
                if (user is not null && HashingHelper.VerifyPassword(userLogin.Password, user.Password))
                {
                    return user;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
                _logger.LogError(ex.Message);
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
                _logger.LogError(e.Message);
                throw new Exception(e.Message);
            }

            return null;
        }

        public async Task<List<AccountManageResponseDto>> GetAllAccounts_Manage()
        {
            try
            {
                var accounts = await _unitOfWork.AccountRepository.GetAllAccounts_Manage().ToListAsync().ConfigureAwait(false);
                var result = _mapper.Map<List<AccountManageResponseDto>>(accounts);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> AddUser(AccountAddRequestDto acc_request)
        {
            try
            {
                var account = new Account
                {
                    Username = acc_request.Username,
                    Email = acc_request.Email,
                    Phone = acc_request.Phone,
                    FirstName = acc_request.FirstName,
                    LastName = acc_request.LastName,
                };
                account.Password = HashingHelper.EncryptPassword("1234ASdf");
                foreach (var roleDto in acc_request.Roles)
                {
                    var role = _unitOfWork.RoleRepositery.GetRoleById(roleDto.RoleId);
                    account.Roles.Add(role);
                }
                    await _unitOfWork.AccountRepository.AddAsync(account).ConfigureAwait(false);
                    await _unitOfWork.CommitAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return false;
        }

        public async Task<bool> UpdateUser(AccountAddRequestDto user_request, int id)
        {
            var existingAccount = await GetAccountByIdReturnAcc(id);
            if (existingAccount == null)
            {
                return false;
            }
            else
            {
                try
                {
                    existingAccount.Username = user_request.Username;
                    existingAccount.Email = user_request.Email;
                    existingAccount.Phone = user_request.Phone;
                    existingAccount.FirstName = user_request.FirstName;
                    existingAccount.LastName = user_request.LastName;
                    existingAccount.Roles.Clear();

                    foreach (var roleDto in user_request.Roles)
                    {
                        var role = _unitOfWork.RoleRepositery.GetRoleById(roleDto.RoleId);
                        if (role == null)
                        {
                            continue;

                        }
                        existingAccount.Roles.Add(role);
                    }
                     await _unitOfWork.CommitAsync().ConfigureAwait(false);
                    return true;

                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }
            }

            return false;
        }

        public async Task<Account> GetAccountByIdReturnAcc(int id)
        {
            try
            {
                Account value = _unitOfWork.AccountRepository.GetAccountByIdReturnAcc(id);
                if (value != null)
                {
                    return value;
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return null;
        }

        public async Task<bool> updateOtp(string email, string otp,DateTime ExpriseTime)
        {
            try
            {
                Account account = await _unitOfWork.AccountRepository.GetUserByEmail(email).ConfigureAwait(false);
                if (account != null)
                {
                    account.Otp = otp;
                    account.Otpexpiretime = ExpriseTime;
                    _unitOfWork.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task SendEmailAsync(MailRequestDto mailRequest)
        {
            MailRequestDto mail = new MailRequestDto();
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("toeicmastersystem@gmail.com");
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("toeicmastersystem@gmail.com", "rpse lwke ibel lbpt");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public async Task<List<AccountTeacherName>> GetAllTeacher()
        {
            try
            {
                var temp = await _unitOfWork.AccountRepository.GetAllTeacher().ToListAsync().ConfigureAwait(false);
                var result = _mapper.Map<List<AccountTeacherName>>(temp);
                return result;
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public async Task<bool> ChangePassword(ChangePasswordRequestDto request)
        {
            try
            {
                var account = await _unitOfWork.AccountRepository.GetAccountById(request.AccountId)
                    .Where(x => x.IsDeleted == Common.IsNotDelete)
                    .FirstOrDefaultAsync().ConfigureAwait(false);
                if (account == null)
                {
                    return false;
                }
                else
                {
                    if (!string.IsNullOrEmpty(request.OldPassword) && !string.IsNullOrEmpty(request.NewPassword) && !string.IsNullOrEmpty(request.ConfirmPassword))
                    {
                        if (HashingHelper.VerifyPassword(request.OldPassword, account.Password) && request.NewPassword.Equals(request.ConfirmPassword))
                        {
                            account.Password = HashingHelper.EncryptPassword(request.NewPassword);
                            _unitOfWork.Commit();
                            return true;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> UpdateAvatar(UpdateAvatarRequestDto request)
        {
            try
            {
                if (request != null)
                {
                    var account = await _unitOfWork.AccountRepository.GetAccountById(request.AccountId)
                        .Where(x => x.IsDeleted == Common.IsNotDelete)
                        .FirstOrDefaultAsync().ConfigureAwait(false);
                    if (account == null)
                    {
                        return false;
                    }
                    else
                    {
                        string image = "";
                        if (request.Avatar != null)
                        {
                            S3RequestData s3RequestData = new S3RequestData
                            {
                                BucketName = "tas",
                                InputStream = request.Avatar.OpenReadStream(),
                                Name = request.Avatar.FileName,
                            };
                            await _s3StorageService.UploadFileAsync(s3RequestData).ConfigureAwait(false);
                            image = _s3StorageService.GetFileUrlDontExpires(s3RequestData);
                        }

                        account.Avatar = image;
                        _unitOfWork.Commit();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAvatar(int id)
        {
            try
            {
                if (id != 0)
                {
                    var account = await _unitOfWork.AccountRepository.GetAccountById(id).FirstOrDefaultAsync().ConfigureAwait(false);
                    if (account == null)
                    {
                        return false;
                    }
                    if (string.IsNullOrEmpty(account.Avatar))
                    {
                        return false;
                    }
                    account.Avatar = "";
                    _unitOfWork.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<List<Enterprise>> GetAllEnterprise()
        {
            try
            {
                var result = _unitOfWork.AccountRepository.GetAllEnterprise().ToList();
                if (result!=null)
                {
                    return result;
                }
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public string GetEnterpriseNameById(int id)
        {
            try
            {
                var result = _unitOfWork.AccountRepository.GetEnterpriseNameById(id);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }

        public Task<Account> GetUserByEmail(string email)
        {
            try
            {
                var result = _unitOfWork.AccountRepository.GetUserByEmail(email);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
    }
}
