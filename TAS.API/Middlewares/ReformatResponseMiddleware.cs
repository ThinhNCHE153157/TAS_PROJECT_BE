using System.Collections.ObjectModel;
using System.Net;
using TAS.Infrastructure.Helpers;

namespace TAS.API.Middlewares
{
    public class ReformatResponseMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ReformatResponseMiddleware> _logger;

        public ReformatResponseMiddleware(RequestDelegate requestDelegate, ILogger<ReformatResponseMiddleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //try
            //{
            var statusCodes = new Collection<int> { 200, 400, 500 };
            await _requestDelegate(httpContext).ConfigureAwait(false);

            int statusCode = httpContext.Response.StatusCode;
            if (!statusCodes.Contains(statusCode))
            {
                string message = string.Empty;
                string identifer = httpContext.TraceIdentifier;

                switch (statusCode)
                {
                    case 401:
                        _logger.LogWarning($"Code 401: Unauthorized! - RequestId: {identifer}");
                        message = HttpStatusCode.Unauthorized.ToString();
                        break;

                    case 403:
                        _logger.LogWarning($"Code 403: Forbidden! - RequestId: {identifer}");
                        message = HttpStatusCode.Forbidden.ToString();
                        break;

                    case 404:
                        _logger.LogWarning($"Code 404: Resource not found! - RequestId: {identifer}");
                        message = HttpStatusCode.NotFound.ToString();
                        break;

                    case 405:
                        _logger.LogWarning($"Code 405: Method Not Allowed! - RequestId: {identifer}");
                        message = HttpStatusCode.MethodNotAllowed.ToString();
                        break;

                    case 415:
                        _logger.LogWarning($"Code 415: Unsupported Media Type! - RequestId: {identifer}");
                        message = HttpStatusCode.UnsupportedMediaType.ToString();
                        break;

                    default:
                        _logger.LogWarning($"Code 500: Internal server error! - RequestId: {identifer}");
                        message = HttpStatusCode.InternalServerError.ToString();
                        break;
                }

                await httpContext.Response
                                .WriteAsJsonAsync(new ApiFormatResponse(statusCode, new Response(false, message)))
                                .ConfigureAwait(false);

            }
            //}
            //catch (Exception ex)
            //{
            //    if (httpContext.Response.HasStarted)
            //    {
            //        _logger.LogCritical(ex.Message);
            //        throw;
            //    }
            //}
        }
    }
}
