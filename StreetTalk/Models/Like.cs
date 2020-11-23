namespace StreetTalk.Models
{
    public class Like
    {
        public virtual int userId { get; set; }
        public virtual User user { get; set; }
        
        public virtual int postId { get; set; }
        public virtual PublicPost post { get; set; }
    }
}