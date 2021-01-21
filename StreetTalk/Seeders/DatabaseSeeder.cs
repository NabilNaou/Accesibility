using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StreetTalk.Data;

namespace StreetTalk.Seeders
{
    public class DatabaseSeeder
    {
        public static async Task SeedAll(StreetTalkContext context)
        {
            await context.Database.EnsureCreatedAsync();
            
            var seeders = new List<Seeder>
            {
                new RoleSeeder(),
                new UserSeeder(),
                new PostCategorySeeder(),
                new PostSeeder(),
                new LikeSeeder(),
                new CommentSeeder()
            };

            foreach (var seeder in seeders)
            {
                Console.WriteLine("Seeding: {0}", seeder);
                await seeder.Seed(context);
            }
        }
    }
}