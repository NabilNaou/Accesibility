namespace StreetTalk.Seeders
{
    public abstract class Seeder
    {
        protected StreetTalkContext Context;

        public void Seed(StreetTalkContext ctx)
        {
            Context = ctx;

            if (ShouldSeed)
            {
                DoSeed();
                Context.SaveChanges();
            }
        }

        public abstract void DoSeed();
        public abstract bool ShouldSeed { get; }
    }
}