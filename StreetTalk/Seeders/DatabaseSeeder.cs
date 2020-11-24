using System.Collections.Generic;

namespace StreetTalk.Seeders
{
    public class DatabaseSeeder
    {
        private static readonly List<Seeder> Seeders = new List<Seeder>
        {
            new UserSeeder(),
            new PostSeeder()
        };
        
        public static void seedAll(StreetTalkContext context)
        {
            context.Database.EnsureCreated();
            Seeders.ForEach(seeder => seeder.seed(context));
        }
    }
}