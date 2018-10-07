using Microsoft.AspNetCore.Builder;

namespace vk2
{
    public static class MiddleWareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}