using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Xiaobao.PaaS.Portal.Shard.Exceptions
{
    public class BusinessExceptionMiddleware
    {
        private readonly RequestDelegate _next;


        public BusinessExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (BusinessException ex)
            {
                var result = new ResponseResult
                {
                    Msg = ex.Msg,
                    Code = ex.Code == 0 ? 500 : ex.Code
                };
                context.Response.StatusCode = StatusCodes.Status200OK;
                context.Response.ContentType = "application/json; charset=utf-8";
                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(result));
            }
        }
    }
}
