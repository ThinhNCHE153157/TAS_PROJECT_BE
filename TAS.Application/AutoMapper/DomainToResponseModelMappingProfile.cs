using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Domains;
using TAS.Data.Dtos.Responses;
using TAS.Data.Entities;

namespace TAS.Application.AutoMapper
{
    public class DomainToResponseModelMappingProfile : Profile
    {
        public DomainToResponseModelMappingProfile()
        {
            CreateMap<Course,CourseHomepageResponeDto>();
            CreateMap<Course,CourseDashboardResponseDto>();
            CreateMap<Account, AccountHomepageResponeDTO>();
            CreateMap<Course, GetCourseByIdResponseDto>()
                .ForMember(d => d.Tests, dt => dt.MapFrom(src => src.Tests));
            CreateMap<Test, TestDto>();
            CreateMap<Account, GetAccountByIdResponseDto>();
            CreateMap<Account, AccountManageResponseDto>()
                .ForMember(_dto => _dto.RoleNames, dt => dt.MapFrom(src => src.Roles.Select(role => role.RoleName).ToList()));
            //CreateMap<Class, ClassManagementDto>()
            //    .ForMember(
            //        dto => dto.Teacher,
            //        opt => opt.MapFrom(src => src.Accounts
            //            .Where(a => a.Roles.Any(r => r.RoleId == 3))
            //            .Select(a => a.LastName + " " + a.FirstName)
            //            .FirstOrDefault()));

            CreateMap<Class, ClassManagementDto>();
            CreateMap<Account, AccountTeacherName>().ForMember(_dto => _dto.Teacher, dt => dt.MapFrom(src => src.FirstName + ' ' + src.LastName));
        }
    }
}
