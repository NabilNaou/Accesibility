namespace StreetTalk.Models
{
    public class PublicPost : AnonymousPost
    {
        public bool closed { get; set; }
        
        public int reportCount { get; set; }
    }
}