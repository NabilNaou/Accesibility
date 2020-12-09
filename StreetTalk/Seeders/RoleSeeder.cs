using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StreetTalk.Data;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class RoleSeeder : Seeder
    {

        public override bool ShouldSeed => true;

        public override void DoSeed(StreetTalkContext context, IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            if (!roleManager.RoleExistsAsync("Buurtbewoner").Result)
            {
                var role = new IdentityRole { Name = "Buurtbewoner" };
                roleManager.CreateAsync(role).Wait();
            }
        }
    }
}