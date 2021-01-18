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
                roleManager.CreateAsync(new IdentityRole { Name = "Buurtbewoner" }).Wait();
            
            if (!roleManager.RoleExistsAsync("Moderator").Result)
                roleManager.CreateAsync(new IdentityRole { Name = "Moderator" }).Wait();
            
            if (!roleManager.RoleExistsAsync("Administrator").Result)
                roleManager.CreateAsync(new IdentityRole { Name = "Administrator" }).Wait();
            
            if (!roleManager.RoleExistsAsync("Gemeentemedewerker").Result)
                roleManager.CreateAsync(new IdentityRole { Name = "Gemeentemedewerker" }).Wait();
        }
    }
}