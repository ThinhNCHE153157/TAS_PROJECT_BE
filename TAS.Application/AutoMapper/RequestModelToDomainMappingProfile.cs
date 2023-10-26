using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Requests;
using TAS.Data.Entities;

namespace TAS.Application.AutoMapper
{
    public class RequestModelToDomainMappingProfile : Profile
    {
        public RequestModelToDomainMappingProfile()
        {
            CreateMap<UserRegisterRequestDto, Account>();
        }
    }
}
