using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreetTalk.Models
{
    public class Post : Timestamped
    {
        public virtual int Id { get; set; }
        
        [StringLength(64)]
        public virtual string Title { get; set; }
        
        [Column(TypeName = "text")]
        public virtual string Content { get; set; }
        
        public virtual PostPhoto Photo { get; set; }
        
        public virtual int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}