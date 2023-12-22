using System.Diagnostics;

namespace RestaurantAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeMiddleware> _logger;
        private readonly Stopwatch _stopWatch;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _stopWatch = new Stopwatch();
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopWatch.Start();
            await next.Invoke(context);
            _stopWatch?.Stop();

            var elapsedMiliseconds = _stopWatch.ElapsedMilliseconds;
            if (elapsedMiliseconds > 4000)
            {
                _logger.LogWarning($"Request [{context.Request.Method}] at {context.Request.Path} took {elapsedMiliseconds} ms");
            }
        }
    }
}
