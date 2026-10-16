namespace RaceDayApi.Middlewares
{
    public class SessionAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userId = context.Session.GetInt32("UserId");
            var userRole = context.Session.GetString("UserRole");

            if (userId.HasValue)
            {
                context.Items["UserId"] = userId.Value;
                context.Items["UserRole"] = userRole;
            }

            await _next(context);
        }
    }
}