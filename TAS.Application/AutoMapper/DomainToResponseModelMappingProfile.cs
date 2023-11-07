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
            CreateMap<Test,CourseResultResponseDto>();
            CreateMap<TestResult,TestResultDto>();
        }
    }
}
