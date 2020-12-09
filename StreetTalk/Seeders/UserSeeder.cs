using System;
using System.Collections.Generic;
using System.Linq;
using StreetTalk.Data;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class UserSeeder : Seeder
    {
        public override bool ShouldSeed => !Context.User.Any();
        
        public override void DoSeed(StreetTalkContext context, IServiceProvider services)
        {

        }
    }
}