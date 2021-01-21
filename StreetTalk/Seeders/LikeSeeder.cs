using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StreetTalk.Data;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class LikeSeeder : Seeder
    {
        public override bool ShouldSeed => !Context.PublicPost.OfType<PublicPost>().Single(p => p.Id == 1).Likes.Any();

        public override async Task DoSeed(StreetTalkContext context)
        {
            var firstPost = Context.PublicPost.OfType<PublicPost>().Single(p => p.Id == 1);
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
            
            var secondPost = Context.PublicPost.OfType<PublicPost>().Single(p => p.Id == 2);
            //The first user likes the second post
            secondPost.Likes.AddRange(new List<Like>
                {
                    new Like
                    {
                        Post = secondPost,
                        User = Context.User.Skip(0).First()
                    },
                }
            );
        }
    }
}