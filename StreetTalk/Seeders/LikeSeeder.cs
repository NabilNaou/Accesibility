using System.Collections.Generic;
using System.Linq;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class LikeSeeder : Seeder
    {
        public override bool shouldSeed => !Context.Post.OfType<PublicPost>().Single(p => p.Id == 1).Likes.Any();

        public override void DoSeed()
        {
            var firstPost = Context.Post.OfType<PublicPost>().Single(p => p.Id == 1);
            //The second and the third user like the first post
            firstPost.Likes.AddRange(new List<Like>
                {
                    new Like
                    {
                        Post = firstPost,
                        User = Context.User.Single(u => u.Id == 2)
                    },
                    new Like
                    {
                        Post = firstPost,
                        User = Context.User.Single(u => u.Id == 3)
                    }
                }
            );
            
            var secondPost = Context.Post.OfType<PublicPost>().Single(p => p.Id == 2);
            //The first user likes the second post
            secondPost.Likes.AddRange(new List<Like>
                {
                    new Like
                    {
                        Post = secondPost,
                        User = Context.User.Single(u => u.Id == 1)
                    },
                }
            );
        }
    }
}