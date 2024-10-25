namespace PeopleRegistration.Service.HeadersConfiguration
{
    public class CustomHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("X-Custom-Header"))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Missing required header: X-Custom-Header");
                return;
            }

            if (!context.Request.Headers.ContainsKey("X-Another-Header"))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Missing required header: X-Another-Header");
                return;
            }

            await _next(context);
        }
    }
}