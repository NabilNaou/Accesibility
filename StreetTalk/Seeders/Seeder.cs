namespace StreetTalk.Seeders
{
    public abstract class Seeder
    {
        protected StreetTalkContext Context;
        

        public void seed(StreetTalkContext ctx)
        {
            Context = ctx;
            
            if(shouldSeed)
                DoSeed();
            
            Context.SaveChanges();
        }

        public abstract void DoSeed();
        public abstract bool shouldSeed { get; }
    }
}