using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace StreetTalk.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseIpBlacklist(this IApplicationBuilder builder, List<string> blacklist)
        {
            return builder.UseMiddleware<IpBlacklist>(blacklist);
        }
    }
}