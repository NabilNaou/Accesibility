using System.ComponentModel.DataAnnotations;

namespace StreetTalk.Models
{
    public class Comment : Timestamped
    {
        public virtual int id { get; set; }
        
        [StringLength(600)]
        public virtual string content { get; set; }
        
        public virtual int userId { get; set; }
        public virtual User author { get; set; }
        
        public virtual int postId { get; set; }
        public virtual PublicPost post { get; set; }
    }
}