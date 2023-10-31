using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using TAS.Infrastructure.Helpers;

namespace TAS.API.Middlewares
{
    public static class ExceptionCatchMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, NLog.ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.Error($"Code 500: Internal server error! - RequestId: {context.TraceIdentifier} - StackTrace: {contextFeature.Error.StackTrace}");

                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        string message = HttpStatusCode.InternalServerError.ToString();

                        await context.Response
                            .WriteAsJsonAsync(new ApiFormatResponse(context.Response.StatusCode, new Response(false, contextFeature.Error.Message)))
                            .ConfigureAwait(false);
                    }
                });
            });

        }
    }
}
