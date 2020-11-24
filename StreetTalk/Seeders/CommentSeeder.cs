using System.Collections.Generic;
using System.Linq;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class CommentSeeder : Seeder
    {
        public override bool shouldSeed => !Context.Post.OfType<PublicPost>().Single(p => p.Id == 1).Comments.Any();

        public override void DoSeed()
        {
            var firstPost = Context.Post.OfType<PublicPost>().Single(p => p.Id == 1);
            firstPost.Comments.AddRange(new List<Comment>
            {
                new Comment
                {
                    Author = Context.User.Single(u => u.Id == 2),
                    Post = firstPost,
                    Content = "Echt ziek kerel"
                },
                new Comment
                {
                    Author = Context.User.Single(u => u.Id == 3),
                    Post = firstPost,
                    Content = "Kan echt niet"
                }
            });
            
            var secondPost = Context.Post.OfType<PublicPost>().Single(p => p.Id == 2);
            secondPost.Comments.AddRange(new List<Comment>
            {
                new Comment
                {
                    Author = Context.User.Single(u => u.Id == 1),
                    Post = secondPost,
                    Content = "Wejooowww"
                }
            });
        }
    }
}