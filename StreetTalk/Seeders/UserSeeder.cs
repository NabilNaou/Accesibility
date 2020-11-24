using System;
using System.Collections.Generic;
using System.Linq;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class UserSeeder : Seeder
    {
        public override bool shouldSeed => !Context.users.Any();
        
        public override void DoSeed()
        {
            var rows = new List<User>
            {
                new User
                {
                    id = 1,
                    email = "19097530@student.hhs.nl",
                    passwordHash = "2dcf1b5dbc35a2d58504cc6c4c7caa589a7c32497f70facaa56c51605f6e45b5",
                    profile = new Profile
                    {
                        firstName = "Timon",
                        lastName = "Landmeter",
                        dateOfBirth = new DateTime(1999, 10, 7)
                    }
                },
                new User
                {
                    id = 2,
                    email = "19083181@student.hhs.nl",
                    passwordHash = "2dcf1b5dbc35a2d58504cc6c4c7caa589a7c32497f70facaa56c51605f6e45b5",
                    profile = new Profile
                    {
                        firstName = "Jelle",
                        lastName = "Krupe",
                    }
                },
                new User
                {
                    id = 3,
                    email = "19050380@student.hhs.nl",
                    passwordHash = "2dcf1b5dbc35a2d58504cc6c4c7caa589a7c32497f70facaa56c51605f6e45b5",
                    profile = new Profile
                    {
                        firstName = "Rik",
                        lastName = "Helder",
                    }
                },
                new User
                {
                    id = 4,
                    email = "19037570@student.hhs.nl",
                    passwordHash = "2dcf1b5dbc35a2d58504cc6c4c7caa589a7c32497f70facaa56c51605f6e45b5",
                    profile = new Profile
                    {
                        firstName = "Nabil",
                        lastName = "Naou",
                    }
                }
            };

            Context.users.AddRange(rows);
        }
    }
}