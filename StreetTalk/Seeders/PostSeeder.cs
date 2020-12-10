using System;
using System.Collections.Generic;
using System.Linq;
using StreetTalk.Data;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class PostSeeder : Seeder
    {
        public override bool ShouldSeed => !Context.Post.Any();
        
        public override void DoSeed(StreetTalkContext context, IServiceProvider services)
        {
            var rows = new List<Post>
            {
                new PublicPost
                {
                    Id = 1,
                    Title = "Overlast touristen",
                    Content = SeederUtils.LoremIpsum,
                    Photo = new PostPhoto
                    {
                        Sensitive = false,
                        Photo = new Photo
                        {
                            Id = 500,
                            Filename = "https://assets.nos.nl/data/image/2017/11/23/433105/xxl.jpg",
                        }
                    },
                    User = Context.User.Skip(0).First()
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
                CreateGarbagePost(12, 2),
                new AnonymousPost
                {
                    Id = 13,
                    Title = "Wiet kwekerij bij de buren",
                    Content = SeederUtils.LoremIpsum,
                    Photo = new PostPhoto
                    {
                        Sensitive = false,
                        Photo = new Photo
                        {
                            Id = 501,
                            Filename = "https://www.nydailynews.com/resizer/Syhve42srvQoXzEPE6ToPsIXBec=/800x1066/top/arc-anglerfish-arc2-prod-tronc.s3.amazonaws.com/public/3XUJPSVHKIUBKZJCFU2WQFA7WY.jpg",
                        }
                    },
                    Pseudonym = "AyZgjE"
                }
            };

            Context.Post.AddRange(rows);
        }

        private PublicPost CreateGarbagePost(int id, int userId)
        {
            return new PublicPost
            {
                Id = id,
                Title = "Afval op straat",
                Content = SeederUtils.LoremIpsum,
                Closed = true,
                Photo = new PostPhoto
                {
                    Sensitive = true,
                    Photo = new Photo
                    {
                        Id = 1000 + id,
                        Filename = "https://upload.wikimedia.org/wikipedia/commons/1/14/Klein_gevaarlijk_afval_A.jpg",
                    }
                },
                User = Context.User.Skip(userId - 1).First()
            };
        }
    }
}