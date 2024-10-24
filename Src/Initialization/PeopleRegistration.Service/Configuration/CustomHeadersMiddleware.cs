namespace Application.Common.Extensions.Middlewares
{
    public class CustomHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Append("X-Custom-Header", "MyCustomHeaderValue");
                context.Response.Headers.Append("X-Powered-By", "ASP.NET Core");

                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}