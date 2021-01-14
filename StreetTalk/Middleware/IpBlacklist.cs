using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace StreetTalk.Middleware
{
    public class IpBlacklist
    {
        private readonly RequestDelegate next;
        private readonly List<string> blacklist;
        
        public IpBlacklist(RequestDelegate next, List<string> blacklist)
        {
            this.next = next;
            this.blacklist = blacklist;
        }

        public async Task Invoke(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress.ToString();

            if (blacklist.Any(line => ip == line))
            {
                context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                return;
            }

            await next.Invoke(context);
        }
    }
}