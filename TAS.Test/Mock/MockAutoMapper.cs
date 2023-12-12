using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Application.AutoMapper;

namespace TAS.Test.Mock
{
    public static class MockAutoMapper
    { 
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(DomainToResponseModelMappingProfile));
                cfg.AddProfile(typeof(RequestModelToDomainMappingProfile));
                cfg.AddProfile(typeof(ResponseModelToDomainMappingProfile));
            });
            return config.CreateMapper();
        }
    }
}
