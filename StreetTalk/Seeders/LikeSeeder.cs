using System.Collections.Generic;
using System.Linq;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class LikeSeeder : Seeder
    {
        public override bool shouldSeed => !Context.Post.OfType<PublicPost>().First().Likes.Any();

        public override void DoSeed()
        {
            var firstPost = Context.Post.OfType<PublicPost>().First();
            //The second and the third user like the first post
            firstPost.Likes.AddRange(new List<Like>
                {
                    new Like
                    {
                        Post = firstPost,
                        User = Context.User.Skip(1).First()
                    },
                    new Like
                    {
                        Post = firstPost,
                        User = Context.User.Skip(2).First()
                    }
                }
            );
            
            var secondPost = Context.Post.OfType<PublicPost>().Skip(1).First();
            //The first user likes the second post
            secondPost.Likes.AddRange(new List<Like>
                {
                    new Like
                    {
                        Post = secondPost,
                        User = Context.User.First()
                    },
                }
            );
        }
    }
}