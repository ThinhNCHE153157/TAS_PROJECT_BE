using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using static TAS.Infrastructure.Enums.SystemEnum;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Text;
using TAS.Application.AutoMapper;
using TAS.Application.Services.Interfaces;
using TAS.Application.Services;
using TAS.Data.EF;
using TAS.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using TAS.Infrastructure.Helpers;
using TAS.Data.Entities;
using Amazon.S3;
using TAS.Data.Dtos.Domains;

namespace TAS.API.DependencyConfig
{
    public static class DependencyInjection
    {
        public static IServiceCollection CommonService(this IServiceCollection services, IConfiguration configuration)
        {
            //HttpContextAcessor
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<TASContext>((provider, option) => 
            {
                option.UseSqlServer(configuration.GetConnectionString("Default"));
                option.AddInterceptors(new SoftDateInterceptor(provider.GetService<IHttpContextAccessor>()!));
             });
                                                            //o => o.MigrationsAssembly("TAS.Data.EF")).AddInterceptors(new SoftDateInterceptor(provider.GetService<IHttpContextAccessor>()!)), ServiceLifetime.Scoped);

            //Config CORS
            services.AddCors(cors => cors.AddPolicy("TasPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = AppSettings.ValidAudience,
                        ValidIssuer = AppSettings.ValidIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AccessTokenSecret"]))
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminRole",
                    policy => policy.RequireRole(UserRoles.Admin.ToString(), UserRoles.Enterprise.ToString(), UserRoles.Teacher.ToString(), UserRoles.Student.ToString()));

                options.AddPolicy("EnterpriseRole",
                    policy => policy.RequireRole(UserRoles.Enterprise.ToString(), UserRoles.Teacher.ToString(), UserRoles.Student.ToString()));

                options.AddPolicy("TeacherRole",
                    policy => policy.RequireRole(UserRoles.Teacher.ToString(), UserRoles.Student.ToString()));
                options.AddPolicy("StudentRole",
                    policy => policy.RequireRole(UserRoles.Student.ToString()));
            });

            // Add services to the container.
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    //options.JsonSerializerOptions.MaxDepth = 5;
                }).ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        return ApiFormatError.CustomErrorResponse(context);
                    };
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "TAS Api",

                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
            });
            });
            services.AddHealthChecks();

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set default value is: 30 MB
            });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = long.MaxValue; // if don't set default value is: 128 MB
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });

            return services;
        }

        public static IServiceCollection AutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<DomainToResponseModelMappingProfile>();
                config.AddProfile<RequestModelToDomainMappingProfile>();
                config.AddProfile<ResponseModelToDomainMappingProfile>();
            });

            services.AddScoped<IMapper>(builder => new Mapper(builder.GetRequiredService<AutoMapper.IConfigurationProvider>(), builder.GetService));
            return services;
        }

        public static IServiceCollection ProfilerService(this IServiceCollection services)
        {
            //Config Profiler
            services.AddMemoryCache();
            //URL: profiler/results-index
            services.AddMiniProfiler(config => config.RouteBasePath = "/profiler")
                .AddEntityFramework();

            return services;
        }

        public static IServiceCollection SignalRService(this IServiceCollection services)
        {
            services.AddSignalR();
            return services;
        }

        public static IServiceCollection ApplicationService(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IS3StorageService,S3StorageService>();
            services.AddScoped<IVnPayService, VnPayService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IVideoService, VideoService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection CacheService(this IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(o =>
            {
                o.Configuration = AppSettings.Redis;
            });

            return services;
        }

        //public static IServiceCollection HttpClientService(this IServiceCollection services)
        //{
        //    services.AddHttpClient(AppSettings.OmdbName, client =>
        //    {
        //        client.BaseAddress = new Uri(AppSettings.OmdbUrl);
        //        client.DefaultRequestHeaders.Accept.Add(
        //            new MediaTypeWithQualityHeaderValue("application/json"));
        //    });

        //    return services;
        //}
    }
}
