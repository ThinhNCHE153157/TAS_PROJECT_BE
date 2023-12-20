using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF;
using TAS.Data.Entities;

namespace TAS.Application.Services
{
    public class ClassService : IClassService
    {
        private readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public ClassService (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ClassManagementDto>> GetAllClassesManage()
        {
            try
            {
                var listClass = await _unitOfWork.ClassRepository.GetAllClassesManage().ToListAsync().ConfigureAwait(false);
                var result = _mapper.Map<List<ClassManagementDto>>(listClass).ToList();
                return result;
            }catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> AddClass(ClassAddNewClassDto classAddNewClassDto)
        {
            try
            {
                var newClass = _mapper.Map<Class>(classAddNewClassDto);
                await _unitOfWork.ClassRepository.AddAsync(newClass).ConfigureAwait(false);
                await _unitOfWork.CommitAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public Task<bool> EditClass(int id, ClassManagementDto classManagementDto)
        {
            throw new NotImplementedException();
        }


        public async Task<List<ClassManagementDto>> GetClassByStudentId(int studentId)
        {
            try
            {
                var list = await _unitOfWork.ClassRepository.GetClassByStudentId(studentId).ToListAsync().ConfigureAwait(false); ;
                var result = _mapper.Map<List<ClassManagementDto>>(list);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ClassCodeDto>> GetClassCode()
        {


            try
            {
                var list = await _unitOfWork.ClassRepository.GetClassCode()!.ToListAsync().ConfigureAwait(false);
                List<ClassCodeDto> result = new List<ClassCodeDto>();
                foreach (var item in list)
                {
                    result.Add(new ClassCodeDto
                    {
                        ClassId = item.ClassId,
                        ClassCode = item.ClassCode,
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> AddStudentIntoClass(string classCode, int accountId)
        {
            var existingAccount = await GetAccountByIdReturnAcc(accountId);
            if (existingAccount == null)
            {
                return false;
            }
            else
            {
                try
                {

                    var c = _unitOfWork.ClassRepository.GetClassByClassCode(classCode);
                    existingAccount.Classes.Add(c);
                    _unitOfWork.Commit();
                    return true;

                }
                catch (Exception e)
                {
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

            }
            return null;
        }
    }
}
