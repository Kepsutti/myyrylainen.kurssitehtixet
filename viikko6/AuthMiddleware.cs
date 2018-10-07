using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace vk2
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ApiKey apiKey;

        public AuthMiddleware(RequestDelegate next, ApiKey key){
            _next = next;
            apiKey = key;
        }

        public async Task Invoke(HttpContext context){
            var headers = context.Request.Headers;
            var claimsPrincipal = new System.Security.Claims.ClaimsPrincipal();

            if (!headers.ContainsKey("x-api-key"))
            {
                context.User = claimsPrincipal;
                context.Response.StatusCode = 400;
            }
            else
            {
                if (headers["x-api-key"] == apiKey.key)
                {
                    var claim = new Claim(ClaimTypes.Role, "User");
                    var identity = new ClaimsIdentity();
                    identity.AddClaim(claim);
                    claimsPrincipal.AddIdentity(identity);
                    context.User = claimsPrincipal;

                    await _next(context);
                    return;
                }
                else if (headers["x-api-key"] == apiKey.adminKey) 
                {
                    var claim = new Claim(ClaimTypes.Role, "Admin");
                    var identity = new ClaimsIdentity();
                    identity.AddClaim(claim);
                    claimsPrincipal.AddIdentity(identity);
                    context.User = claimsPrincipal;
                    
                    await _next(context);
                    return;
                }
                else
                {
                    context.Response.StatusCode = 403;
                }
}
            //var watch = new Stopwatch();
            //watch.Start();
            //await _next(context);
            //context.Response.Headers.Add("X-Processing-Time-Milliseconds", 
            //new[] { watch.ElapsedMilliseconds.ToString() });
        }
    }
}