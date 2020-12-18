using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public override void DoSeed(StreetTalkContext context)
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

            if (roleManager.RoleExistsAsync("Buurtbewoner").Result) return;
            
            var role = new IdentityRole { Name = "Buurtbewoner" };
            roleManager.CreateAsync(role).Wait();
        }
    }
}