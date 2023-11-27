using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.Entities;

namespace TAS.Application.Services.Interfaces
{
    public interface IClassService
    {
        public Task<List<ClassManagementDto>> GetAllClassesManage();
        public Task<bool> AddClass(ClassAddNewClassDto classAddNewClassDto);
        public Task<bool> EditClass(int id, ClassManagementDto classManagementDto);
        public Task<bool> AddStudentIntoClass(string classCode, int accountId);
        public Task<List<ClassManagementDto>> GetClassByStudentId(int studentId);
        public Task<List<ClassCodeDto>> GetClassCode();
    }
}
