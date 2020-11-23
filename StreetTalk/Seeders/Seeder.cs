namespace StreetTalk.Seeders
{
    public abstract class Seeder
    {
        protected StreetTalkContext context;
        
        public void seed(StreetTalkContext context)
        {
            this.context = context;
            OnSeed();
            context.SaveChanges();
        }

        public abstract void OnSeed();
    }
}