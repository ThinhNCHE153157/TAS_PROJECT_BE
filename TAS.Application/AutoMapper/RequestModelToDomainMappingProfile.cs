using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Domains;
using TAS.Data.Dtos.Requests;
using TAS.Data.Entities;
using static TAS.Infrastructure.Enums.SystemEnum;

namespace TAS.Application.AutoMapper
{
    public class RequestModelToDomainMappingProfile : Profile
    {
        public RequestModelToDomainMappingProfile()
        {
            CreateMap<UserRegisterRequestDto, Account>();
            CreateMap<AccountAddRequestDto, Account>();
            CreateMap<RoleRequestDto, Role>()
                .ForMember(d => d.RoleName, opt => opt.MapFrom(src =>(UserRoles)src.RoleId));
            CreateMap<AddCourseRequestDto, Course>();
            CreateMap<UpdateQuestionRequestDto, Question>();
            CreateMap<QuestionAnswerDto, QuestionAnswer>();
            CreateMap<CreateQuestionRequestDto, Question>();
            CreateMap<CreateTestRequestDto, Test>();
            CreateMap<AddTopicRequestDto, Topic>();
            CreateMap<AddVideoToTopicRequestDto, Video>();
        }
    }
}
