namespace LibrarySystem.API.Middlewares;
internal class ContentSecurityPolicyMiddleware(RequestDelegate _next)
{
    public async Task Invoke(HttpContext context)
    {
        // context.Response.Headers.Append("Content-Security-Policy", "default-src 'self'; connect-src 'self' https://mydom.runasp.net;");
        context.Response.Headers.Append("Content-Security-Policy", "frame-ancestors 'none'");
        // Remove unwanted headers
        context.Response.Headers.Remove("Content-Security-Policy-Report-Only");
        await _next(context);
    }
}
