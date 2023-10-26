using NLog.Web;
using NLog;
using TAS.API.DependencyConfig;
using TAS.API.Middlewares;
using TAS.Data.EF;
using TAS.Data.Entities;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("Initializing...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services
        .CommonService(builder.Configuration)
        .AutoMapperService()
        .ProfilerService()
        .ApplicationService()
        .CacheService();
        //.HttpClientService();

    /*
     Runtime
     */
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        logger.Info("Development environment loaded!");
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseMiniProfiler();
    }

    using (var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope())
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<TASContext>();
        //try
        //{
        //    context.Database
        //        .ExecuteSqlRaw("select exists(\r\n SELECT datname FROM pg_catalog.pg_database WHERE lower(datname) = lower('postgres')\r\n);");
        //}
        //catch (Exception e)
        //{
        //    if (e.Message.Contains("3D000"))
        //    {
        //        context.Database.Migrate();
        //    }
        //}
    }

    logger.Error(app.Environment.IsDevelopment().ToString());

    app.ConfigureExceptionHandler(logger);

    app.UseCors("TasPolicy");

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    //app.UseReformatExtension();

    app.MapHealthChecks("/api/health");

    app.Run();
}
catch (Exception e)
{
    logger.Error(e.Message);
    throw;
}
finally
{
    LogManager.Shutdown();
}

