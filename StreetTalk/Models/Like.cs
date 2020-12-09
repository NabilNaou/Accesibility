namespace StreetTalk.Models
{
    public class Like
    {
        public virtual string UserId { get; set; }
        public virtual StreetTalkUser User { get; set; }
        
        public virtual int PostId { get; set; }
        public virtual PublicPost Post { get; set; }
    }
}