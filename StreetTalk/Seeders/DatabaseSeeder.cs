using System;
using System.Collections.Generic;
using StreetTalk.Data;

namespace StreetTalk.Seeders
{
    public class DatabaseSeeder
    {
        private static readonly List<Seeder> Seeders = new List<Seeder>
        {
            new UserSeeder(),
            new PostSeeder(),
            new LikeSeeder(),
            new CommentSeeder()
        };
        
        public static void SeedAll(StreetTalkContext context)
        {
            context.Database.EnsureCreated();
            Seeders.ForEach(seeder =>
            {
                Console.WriteLine("Seeding: {0}", seeder);
                seeder.Seed(context);
            });
        }
    }
}