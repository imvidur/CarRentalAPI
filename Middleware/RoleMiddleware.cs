using System.Security.Claims;

public class RoleMiddleware
{
    private readonly RequestDelegate _next;

    public RoleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            var role = context.User.FindFirst(ClaimTypes.Role)?.Value;

            // Example: Allow only Admins to access certain routes
            if (context.Request.Path.StartsWithSegments("/admin") && role != "Admin")
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Access Denied");
                return;
            }
        }

        await _next(context);
    }
}
