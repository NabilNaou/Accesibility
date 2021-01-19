using System;
using System.Collections.Generic;
using System.Linq;
using StreetTalk.Data;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class PostSeeder : Seeder
    {
        public override bool ShouldSeed => !Context.PublicPost.Any();
        
        public override void DoSeed(StreetTalkContext context)
        {
            var rows = new List<PublicPost>
            {
                new PublicPost
                {
                    Id = 1,
                    Title = "Overlast touristen",
                    Content = SeederUtils.LoremIpsum,
                    CategoryId = 1,
                    Photo = new PostPhoto
                    {
                        PostId = 1,
                        Sensitive = false,
                        Photo = new Photo
                        {
                            Filename = "https://assets.nos.nl/data/image/2017/11/23/433105/xxl.jpg",
                        }
                    },
                    User = Context.User.OrderBy(u => u.Id).First()
                },
                CreateGarbagePost(2, 1),
                CreateGarbagePost(3, 2),
                CreateGarbagePost(4, 4),
                CreateGarbagePost(5, 3),
                CreateGarbagePost(6, 1),
                CreateGarbagePost(7, 4),
                CreateGarbagePost(8, 2),
                CreateGarbagePost(9, 3),
                CreateGarbagePost(10, 1),
                CreateGarbagePost(11, 4),
                CreateGarbagePost(12, 2)
            };

            Context.PublicPost.AddRange(rows);

            var anonymousRows = new List<AnonymousPost>
            {
                new AnonymousPost
                {
                    Id = 1,
                    Title = "Wiet kwekerij bij de buren",
                    Content = SeederUtils.LoremIpsum,
                    Pseudonym = "AyZgjE"
                }
            };
            
            Context.AnonymousPost.AddRange(anonymousRows);
        }

        private PublicPost CreateGarbagePost(int id, int userId)
        {
            return new PublicPost
            {
                Id = id,
                Title = "Afval op straat",
                CategoryId = 1,
                Content = SeederUtils.LoremIpsum,
                Closed = true,
                Photo = new PostPhoto
                {
                    PostId = id,
                    Sensitive = true,
                    Photo = new Photo
                    {
                        Filename = "https://upload.wikimedia.org/wikipedia/commons/1/14/Klein_gevaarlijk_afval_A.jpg",
                    }
                },
                User = Context.User.OrderBy(u => u.Id).Skip(userId - 1).First()
            };
        }
    }
}