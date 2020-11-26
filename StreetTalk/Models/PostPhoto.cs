namespace StreetTalk.Models
{
    public class PostPhoto
    {
        public virtual bool Sensitive { get; set; }
        
        public virtual int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
        
        public virtual int PostId { get; set; }
        public virtual PublicPost Post { get; set; }
    }
}