using System;
using Microsoft.EntityFrameworkCore;
using StreetTalk.Data;
using StreetTalk.Seeders;

namespace StreetTalkTests
{
    public class BaseTest
    {
        private string databaseName;

        protected StreetTalkContext Db
        {
            get
            {
                var context = GetNewInMemoryDatabase(true);
                DatabaseSeeder.SeedAll(context);
                return GetNewInMemoryDatabase(false);
            }
        }

        private StreetTalkContext GetNewInMemoryDatabase(bool newDb)
        {
            if (newDb)
                databaseName = Guid.NewGuid().ToString();
            
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase(databaseName)
                .Options;
            
            return new StreetTalkContext(options);
        }
        
    }
}