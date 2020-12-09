using System;
using System.Collections.Generic;
using System.Linq;
using StreetTalk.Data;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class CommentSeeder : Seeder
    {
        public override bool ShouldSeed => !Context.Post.OfType<PublicPost>().Single(p => p.Id == 1).Comments.Any();

        public override void DoSeed(StreetTalkContext context, IServiceProvider services)
        {
            var firstPost = Context.Post.OfType<PublicPost>().Single(p => p.Id == 1);
            var secondPost = Context.Post.OfType<PublicPost>().Single(p => p.Id == 2);
            
            firstPost.Comments.Add(
                new Comment
                {
                    AuthorId = "2",
                    PostId = 1,
                    Content = "Echt ziek kerel"
                }
            );

            Context.SaveChanges();

            firstPost.Comments.Add(
                new Comment
                {
                    AuthorId = "3",
                    PostId = 1,
                    Content = "Kan echt niet"
                }
            );
            
            Context.SaveChanges();
            
            secondPost.Comments.Add(
                new Comment
                {
                    AuthorId = "1",
                    PostId = 2,
                    Content = "Wejooowww"
                }
            );
                
            Context.SaveChanges();
        }
    }
}