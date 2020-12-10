using System;
using System.Collections.Generic;
using System.Linq;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class UserSeeder : Seeder
    {
        public override bool ShouldSeed => !Context.User.Any();
        
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
                        Id = 1,
                        FirstName = "Timon",
                        LastName = "Landmeter",
                        DateOfBirth = new DateTime(1999, 10, 7),
                        Photo = new ProfilePhoto
                        {
                            Photo = new Photo
                            {
                                Id = 1,
                                Filename = "https://picsum.photos/seed/1/256/256"
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
                        Id = 2,
                        FirstName = "Jelle",
                        LastName = "Krupe",
                        Photo = new ProfilePhoto
                        {
                            Photo = new Photo
                            {
                                Id = 2,
                                Filename = "https://picsum.photos/seed/2/256/256"
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
                        Id = 3,
                        FirstName = "Rik",
                        LastName = "Helder",
                        Photo = new ProfilePhoto
                        {
                            Photo = new Photo
                            {
                                Id = 3,
                                Filename = "https://picsum.photos/seed/3/256/256"
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
                        Id = 4,
                        FirstName = "Nabil",
                        LastName = "Naou",
                        Photo = new ProfilePhoto
                        {
                            Photo = new Photo
                            {
                                Id = 4,
                                Filename = "https://picsum.photos/seed/4/256/256"
                            }
                        }
                    }
                }
            };

            Context.User.AddRange(rows);
        }
    }
}