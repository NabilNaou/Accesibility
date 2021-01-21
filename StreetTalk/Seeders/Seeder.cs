using StreetTalk.Data;
using System.Threading.Tasks;

namespace StreetTalk.Seeders
{
    public abstract class Seeder
    {
        protected StreetTalkContext Context;

        public async Task Seed(StreetTalkContext ctx)
        {
            Context = ctx;

            if (ShouldSeed)
            {
                await DoSeed(Context);
                await Context.SaveChangesAsync();
            }
        }

        public abstract Task DoSeed(StreetTalkContext context);
        public abstract bool ShouldSeed { get; }
    }
}