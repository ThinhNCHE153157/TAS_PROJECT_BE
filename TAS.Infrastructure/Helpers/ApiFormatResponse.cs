namespace TAS.Infrastructure.Helpers
{
    public class ApiFormatResponse
    {
        public int Code { get; set; }
        public Response Response { get; set; }

        public ApiFormatResponse(int code, Response response)
        {
            Code = code;
            Response = response;
        }
    }

    public class Response
    {
        public bool Success { get; set; }

        public object? Data { get; set; }

        public Response(bool success, object? data)
        {
            Success = success;
            Data = data;
        }
    }
}
