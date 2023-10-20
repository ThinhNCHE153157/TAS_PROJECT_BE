using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RequestModelToDomainMappingProfile());
                cfg.AddProfile(new DomainToResponseModelMappingProfile());
                cfg.AddProfile(new ResponseModelToDomainMappingProfile());
            });
        }
    }
}
