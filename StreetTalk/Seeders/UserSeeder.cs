using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StreetTalk.Data;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class UserSeeder : Seeder
    {
        private UserStore<StreetTalkUser> userStore;
        
        public override bool ShouldSeed => !Context.User.Any();
        
        public override async Task DoSeed(StreetTalkContext context)
        {
            userStore = new UserStore<StreetTalkUser>(context);
            await CreateUser("Timon", "Landmeter", "19097530@student.hhs.nl", "Qwerty123!", "Buurtbewoner");
            await CreateUser("Jelle", "Krupe", "19083181@student.hhs.nl", "Qwerty123!", "Buurtbewoner");
            await CreateUser("Rik", "Helder", "19050380@student.hhs.nl", "Qwerty123!", "Buurtbewoner");
            await CreateUser("Nabil", "Naou", "19037570@student.hhs.nl", "Qwerty123!", "Buurtbewoner");
            
            await CreateUser("Moderator", "", "moderator@streettalk.nl", "Qwerty123!", "Moderator");
            await CreateUser("Administrator", "", "administrator@streettalk.nl", "Qwerty123!", "Administrator");
            await CreateUser("Gemeentemedewerker", "", "gemeentemedewerker@streettalk.nl", "Qwerty123!", "Gemeentemedewerker");
        }

        private async Task CreateUser(string firstName, string lastName, string email, string password, string role)
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
                LastKnownIpAddress = "",
                Profile = new Profile
                {
                    DateOfBirth = DateTime.Now,
                    City = "",
                    Street = "",
                    HouseNumber = 0,
                    HouseNumberAddition = "",
                    PostalCode = "",
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

            user.LockoutEnabled = role == "Buurtbewoner";
            
            var passwordHasher = new PasswordHasher<StreetTalkUser>();
            var hashed = passwordHasher.HashPassword(user, password);
            user.PasswordHash = hashed;
            
            await userStore.CreateAsync(user);
            await userStore.AddToRoleAsync(user, role);
        }
    }
}