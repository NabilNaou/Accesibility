using StreetTalk.Data;
using System;

namespace StreetTalk.Seeders
{
    public abstract class Seeder
    {
        protected StreetTalkContext Context;

        public void Seed(StreetTalkContext ctx, IServiceProvider services)
        {
            Context = ctx;

            if (ShouldSeed)
            {
                DoSeed(Context, services);
                Context.SaveChanges();
            }
        }

        public abstract void DoSeed(StreetTalkContext context, IServiceProvider services);
        public abstract bool ShouldSeed { get; }
    }
}