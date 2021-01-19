using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using StreetTalk.Data;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class RoleSeeder : Seeder
    {

        public override bool ShouldSeed => true;

        public override async Task DoSeed(StreetTalkContext context)
        {
            var validators = new Collection<IRoleValidator<IdentityRole>>
            {
                new RoleValidator<IdentityRole>()
            };
            
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole, StreetTalkContext>(context), 
                validators,
                null, 
                null, 
                null);

            if (!roleManager.RoleExistsAsync("Buurtbewoner").Result)
                await roleManager.CreateAsync(new IdentityRole { Name = "Buurtbewoner" });
            
            if (!roleManager.RoleExistsAsync("Moderator").Result)
                await roleManager.CreateAsync(new IdentityRole { Name = "Moderator" });
            
            if (!roleManager.RoleExistsAsync("Administrator").Result)
                await roleManager.CreateAsync(new IdentityRole { Name = "Administrator" });
            
            if (!roleManager.RoleExistsAsync("Gemeentemedewerker").Result)
                await roleManager.CreateAsync(new IdentityRole { Name = "Gemeentemedewerker" });
        }
    }
}