using StreetTalk.Data;
using System;

namespace StreetTalk.Seeders
{
    public abstract class Seeder
    {
        protected StreetTalkContext Context;

        public void Seed(StreetTalkContext ctx)
        {
            Context = ctx;

            if (!ShouldSeed) return;
            
            DoSeed(Context);
            Context.SaveChanges();
        }

        public abstract void DoSeed(StreetTalkContext context);
        public abstract bool ShouldSeed { get; }
    }
}