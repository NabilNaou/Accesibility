using System;
using System.Collections.Generic;
using System.Linq;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class UserSeeder : Seeder
    {
        public override bool shouldSeed => !Context.User.Any();
        
        public override void DoSeed()
        {
            var rows = new List<User>
            {
                new User
                {
                    Id = 1,
                    Email = "19097530@student.hhs.nl",
                    PasswordHash = "2dcf1b5dbc35a2d58504cc6c4c7caa589a7c32497f70facaa56c51605f6e45b5",
                    Profile = new Profile
                    {
                        FirstName = "Timon",
                        LastName = "Landmeter",
                        DateOfBirth = new DateTime(1999, 10, 7),
                        Photo = new ProfilePhoto
                        {
                            Photo = new Photo
                            {
                                Filename = "https://cdn.discordapp.com/emojis/749350114589671544.png?v=1"
                            }
                        }
                    }
                },
                new User
                {
                    Id = 2,
                    Email = "19083181@student.hhs.nl",
                    PasswordHash = "2dcf1b5dbc35a2d58504cc6c4c7caa589a7c32497f70facaa56c51605f6e45b5",
                    Profile = new Profile
                    {
                        FirstName = "Jelle",
                        LastName = "Krupe",
                        Photo = new ProfilePhoto
                        {
                            Photo = new Photo
                            {
                                Filename = "https://cdn.discordapp.com/emojis/749350114589671544.png?v=1"
                            }
                        }
                    }
                },
                new User
                {
                    Id = 3,
                    Email = "19050380@student.hhs.nl",
                    PasswordHash = "2dcf1b5dbc35a2d58504cc6c4c7caa589a7c32497f70facaa56c51605f6e45b5",
                    Profile = new Profile
                    {
                        FirstName = "Rik",
                        LastName = "Helder",
                        Photo = new ProfilePhoto
                        {
                            Photo = new Photo
                            {
                                Filename = "https://cdn.discordapp.com/emojis/749350114589671544.png?v=1"
                            }
                        }
                    }
                },
                new User
                {
                    Id = 4,
                    Email = "19037570@student.hhs.nl",
                    PasswordHash = "2dcf1b5dbc35a2d58504cc6c4c7caa589a7c32497f70facaa56c51605f6e45b5",
                    Profile = new Profile
                    {
                        FirstName = "Nabil",
                        LastName = "Naou",
                        Photo = new ProfilePhoto
                        {
                            Photo = new Photo
                            {
                                Filename = "https://cdn.discordapp.com/emojis/749350114589671544.png?v=1"
                            }
                        }
                    }
                }
            };

            Context.User.AddRange(rows);
        }
    }
}