using System.ComponentModel.DataAnnotations;

namespace StreetTalk.Models
{
    public class Comment : Timestamped
    {
        public virtual int Id { get; set; }
        
        [StringLength(600)]
        public virtual string Content { get; set; }
        
        public virtual string AuthorId { get; set; }
        public virtual StreetTalkUser Author { get; set; }
        
        public virtual int PostId { get; set; }
        public virtual PublicPost Post { get; set; }
    }
}