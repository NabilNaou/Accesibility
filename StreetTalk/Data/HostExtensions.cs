using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StreetTalk.Seeders;

namespace StreetTalk
{
    public static class HostExtensions
    {
        public static IHost SeedData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var ctx = services.GetService<StreetTalkContext>();
            ctx.Database.Migrate();
            DatabaseSeeder.seedAll(ctx);

            return host;
        }
    }
}