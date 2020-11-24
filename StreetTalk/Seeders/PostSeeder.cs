using System.Collections.Generic;
using System.Linq;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class PostSeeder : Seeder
    {
        public override bool shouldSeed => !Context.posts.Any();
        
        public override void DoSeed()
        {
            var rows = new List<Post>
            {
                new Post
                {
                    title = "Overlast touristen",
                    content = "lorem ipsum dolor sit amet",
                    photo = new Photo
                    {
                        filename = "https://assets.nos.nl/data/image/2017/11/23/433105/xxl.jpg",
                        sensitive = false
                    },
                    user = Context.users.First()
                },
                new Post
                {
                    title = "Afval op straat",
                    content = "lorem ipsum dolor sit amet",
                    photo = new Photo
                    {
                        filename = "https://upload.wikimedia.org/wikipedia/commons/1/14/Klein_gevaarlijk_afval_A.jpg",
                        sensitive = false
                    },
                    user = Context.users.Skip(1).First()
                }
            };

            Context.posts.AddRange(rows);
        }
    }
}