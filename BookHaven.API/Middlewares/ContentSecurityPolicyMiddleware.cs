namespace BookHaven.API.Middlewares;
// RequestDelegate is a delegate that is used to process an HTTP request and produce an HTTP response. It is typically used within middleware components to handle the request and response lifecycle.
internal class ContentSecurityPolicyMiddleware(RequestDelegate _next)
{
    public async Task Invoke(HttpContext context)
    {
        // Logic before calling the next middleware
        context.Response.Headers.Append(
                                    "Content-Security-Policy",
                                    "default-src 'self'; https://bookshaven.runasp.net; connect-src 'self'; frame-ancestors 'none'");
        // Remove unwanted headers
        context.Response.Headers.Remove("Content-Security-Policy-Report-Only");
        await _next(context);
        // Call the next middleware in the pipeline
    }
}
