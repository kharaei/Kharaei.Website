namespace Kharaei.CMS.Middlewares;

public class RedirectMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Host.Host.Contains("arcomp"))
            context.Response.Redirect("https://killedbyme.kharaei.ir/arcomp", true);
        else
            await next(context);
    }
}