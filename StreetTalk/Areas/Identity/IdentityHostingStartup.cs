using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StreetTalk.Data;
using StreetTalk.Models;

[assembly: HostingStartup(typeof(StreetTalk.Areas.Identity.IdentityHostingStartup))]
namespace StreetTalk.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
/*            builder.ConfigureServices((context, services) => {
                services.AddDbContext<StreetTalkContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("StreetTalkContextConnection")));

                services.AddDefaultIdentity<StreetTalkUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<StreetTalkContext>();
            });*/
        }
    }
}