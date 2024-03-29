﻿using AutoMapper;
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
            //CreateMap<ClassAddNewClassDto, Class>();
            CreateMap<RoleRequestDto, Role>()
                .ForMember(d => d.RoleName, opt => opt.MapFrom(src =>(UserRoles)src.RoleId));
            CreateMap<AddCourseRequestDto, Course>();
            CreateMap<UpdateQuestionRequestDto, Question>();
            CreateMap<QuestionAnswerDto, QuestionAnswer>();
            CreateMap<CreateQuestionRequestDto, Question>();
            CreateMap<CreateTestRequestDto, Test>();
            CreateMap<AddTopicRequestDto, Topic>();
            CreateMap<AddVideoOfTopicRequestDto, Video>();
            CreateMap<AddVideoToTopicRequestDto, Video>();
            CreateMap<UpdateVideoRequestDto, Video>();
            CreateMap<SaveTestResultRequestDto, TestResult>()
                .ForMember(d => d.TestId, opt => opt.MapFrom(src => src.TestId))
                .ForMember(d => d.AccountId, opt => opt.MapFrom(src => src.AccountId))
                .ForMember(d => d.TestFinish, opt => opt.MapFrom(src => src.TestFinish))
                .ForMember(d => d.TestScore, opt => opt.MapFrom(src => src.TestScore))
                .ForMember(d => d.TestNumberCorrect, opt => opt.MapFrom(src => Int32.Parse(src.TestNumberCorrect)));
            CreateMap<FlashCardRequestDto, Flashcard>();
            CreateMap<AddItemCardRequestDto,ItemCard>();
            CreateMap<PartTestDto, Part>();
            CreateMap<GetQuestionAnswerDto, QuestionAnswer>();
            CreateMap<AddFlashCardItemRequestDto, ItemCard>();
            CreateMap<AddEnterpriseRequestDto, Enterprise>();
        }
    }
}
