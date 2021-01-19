using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StreetTalk.Data;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class PostCategorySeeder : Seeder
    {
        public override bool ShouldSeed => !Context.PostCategory.Any();
        
        public override async Task DoSeed(StreetTalkContext context)
        {
            var rows = new List<PostCategory>
            {
                new PostCategory
                {
                    Id = 1,
                    Name = "Overlast"
                },
                new PostCategory
                {
                    Id = 2,
                    Name = "Overig"
                }
            };
            
            await Context.PostCategory.AddRangeAsync(rows);
        }
    }
}