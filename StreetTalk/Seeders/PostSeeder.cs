using System.Collections.Generic;
using System.Linq;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class PostSeeder : Seeder
    {
        public override bool shouldSeed => !Context.Post.Any();
        
        public override void DoSeed()
        {
            var rows = new List<Post>
            {
                new PublicPost
                {
                    Id = 1,
                    Title = "Overlast touristen",
                    Content = "lorem ipsum dolor sit amet",
                    Photo = new PostPhoto
                    {
                        Sensitive = false,
                        Photo = new Photo
                        {
                            Filename = "https://assets.nos.nl/data/image/2017/11/23/433105/xxl.jpg",
                        }
                    },
                    User = Context.User.Single(u => u.Id == 1)
                },
                new PublicPost
                {
                    Id = 2,
                    Title = "Afval op straat",
                    Content = "lorem ipsum dolor sit amet",
                    Photo = new PostPhoto
                    {
                        Sensitive = true,
                        Photo = new Photo
                        {
                            Filename = "https://upload.wikimedia.org/wikipedia/commons/1/14/Klein_gevaarlijk_afval_A.jpg",
                        }
                    },
                    User = Context.User.Single(u => u.Id == 2)
                },
                new AnonymousPost
                {
                    Id = 3,
                    Title = "Wiet kwekerij bij de buren",
                    Content = "lorem ipsum dolor sit amet",
                    Photo = new PostPhoto
                    {
                        Sensitive = false,
                        Photo = new Photo
                        {
                            Filename = "https://www.nydailynews.com/resizer/Syhve42srvQoXzEPE6ToPsIXBec=/800x1066/top/arc-anglerfish-arc2-prod-tronc.s3.amazonaws.com/public/3XUJPSVHKIUBKZJCFU2WQFA7WY.jpg",
                        }
                    },
                    Pseudonym = "AyZgjE"
                }
            };

            Context.Post.AddRange(rows);
        }
    }
}