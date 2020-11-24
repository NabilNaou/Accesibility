using System.ComponentModel.DataAnnotations;

namespace StreetTalk.Models
{
    public class Comment : Timestamped
    {
        public virtual int Id { get; set; }
        
        [StringLength(600)]
        public virtual string Content { get; set; }
        
        public virtual int AuthorId { get; set; }
        public virtual User Author { get; set; }
        
        public virtual int PostId { get; set; }
        public virtual PublicPost Post { get; set; }
    }
}