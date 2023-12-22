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
            CreateMap<Test, TestDto>();
            CreateMap<Account, GetAccountByIdResponseDto>();
            CreateMap<Account, AccountManageResponseDto>()
                .ForMember(_dto => _dto.RoleNames, dt => dt.MapFrom(src => src.Roles.Select(role => role.RoleName).ToList()));


            CreateMap<Class, ClassManagementDto>();
            CreateMap<Account, AccountTeacherName>().ForMember(_dto => _dto.Teacher, dt => dt.MapFrom(src => src.FirstName + ' ' + src.LastName));

            CreateMap<Test,CourseResultResponseDto>();
            CreateMap<TestResult,TestResultDto>();
            CreateMap<QuestionAnswer, QuestionAnswerDto>();
            CreateMap<Test,GetTestByIdResponseDto>();
            CreateMap<Test,GetListTestFreeResponseDto>();
            CreateMap<Test, GetAllTestResponseDto>();
            CreateMap<Topic,GetTopicByCourseIdResponseDto>();
            CreateMap<Video,VideoDto>();
            CreateMap<Topic,TopicDto>();
            CreateMap<Course,GetCourseByIdResponseDto>();
            CreateMap<Part, PartDto>(); 
            CreateMap<Question, QuestionDto>();
        }
    }
}
