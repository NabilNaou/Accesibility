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
    public class UserSeeder : Seeder
    {
        private UserStore<StreetTalkUser> userStore;
        
        public override bool ShouldSeed => !Context.User.Any();
        
        public override void DoSeed(StreetTalkContext context)
        {
            userStore = new UserStore<StreetTalkUser>(context);
            CreateUser("Timon", "Landmeter", "19097530@student.hhs.nl", "Qwerty123!", "Buurtbewoner");
            CreateUser("Jelle", "Krupe", "19083181@student.hhs.nl", "Qwerty123!", "Buurtbewoner");
            CreateUser("Rik", "Helder", "19050380@student.hhs.nl", "Qwerty123!", "Buurtbewoner");
            CreateUser("Nabil", "Naou", "19037570@student.hhs.nl", "Qwerty123!", "Buurtbewoner");
        }

        private void CreateUser(string firstName, string lastName, string email, string password, string role)
        {
            var user = new StreetTalkUser
            {
                UserName = email,
                NormalizedUserName = email,
                Email = email,
                NormalizedEmail = email,
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                Profile = new Profile
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Photo = new ProfilePhoto {
                        Photo = new Photo
                        {
                            Filename = $"https://picsum.photos/seed/{(firstName + lastName).GetHashCode()}/256/256"
                        }
                    }
                }
            };
            
            var passwordHasher = new PasswordHasher<StreetTalkUser>();
            var hashed = passwordHasher.HashPassword(user, password);
            user.PasswordHash = hashed;
            
            userStore.CreateAsync(user).Wait();
            userStore.AddToRoleAsync(user, role).Wait();
        }
    }
}