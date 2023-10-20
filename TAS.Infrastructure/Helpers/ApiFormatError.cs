using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TAS.Infrastructure.Helpers
{
    public static class ApiFormatError
    {
        public static BadRequestObjectResult CustomErrorResponse(ActionContext actionContext)
        {
            return new BadRequestObjectResult(actionContext.ModelState
                .Where(error => error.Value!.Errors.Count > 0)
                .Select(error =>
                {
                    string message = error.Value!.Errors.FirstOrDefault()!.ErrorMessage;
                    return new ApiFormatResponse(
                        StatusCodes.Status400BadRequest,
                        new Response(false, message));
                }).LastOrDefault());
        }
    }
}
