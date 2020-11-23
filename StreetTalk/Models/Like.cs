namespace StreetTalk.Models
{
    public class Like
    {
        public int userId { get; set; }
        public User user { get; set; }
        
        public int postId { get; set; }
        public PublicPost post { get; set; }
    }
}