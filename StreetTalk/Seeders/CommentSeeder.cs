using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StreetTalk.Data;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class CommentSeeder : Seeder
    {
        public override bool ShouldSeed => !Context.PublicPost.OfType<PublicPost>().Single(p => p.Id == 1).Comments.Any();

        public override async Task DoSeed(StreetTalkContext context)
        {
            var firstPost = Context.PublicPost.OfType<PublicPost>().Single(p => p.Id == 1);
            var secondPost = Context.PublicPost.OfType<PublicPost>().Single(p => p.Id == 2);
            
            firstPost.Comments.Add(
                new Comment
                {
                    Author = Context.User.Skip(1).First(),
                    PostId = 1,
                    Content = "Echt ziek kerel"
                }
            );

            Context.SaveChanges();

            firstPost.Comments.Add(
                new Comment
                {
                    Author = Context.User.Skip(2).First(),
                    PostId = 1,
                    Content = "Kan echt niet"
                }
            );
            
            await Context.SaveChangesAsync();
            
            secondPost.Comments.Add(
                new Comment
                {
                    Author = Context.User.Skip(1).First(),
                    PostId = 2,
                    Content = "Wejooowww"
                }
            );
                
            await Context.SaveChangesAsync();
        }
    }
}