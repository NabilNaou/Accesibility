using System.ComponentModel.DataAnnotations;

namespace StreetTalk.Models
{
    public class Comment : Timestamped
    {
        public int id { get; set; }
        
        [StringLength(600)]
        public string content { get; set; }
        
        public int userId { get; set; }
        public User author { get; set; }
        
        public int postId { get; set; }
        public PublicPost post { get; set; }
    }
}