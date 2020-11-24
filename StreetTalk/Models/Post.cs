using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreetTalk.Models
{
    public class Post : Timestamped
    {
        public virtual int id { get; set; }
        
        [StringLength(64)]
        public virtual string title { get; set; }
        
        [Column(TypeName = "text")]
        public virtual string content { get; set; }
        
        public virtual int? photoId { get; set; }
        public virtual Photo photo { get; set; }
        
        public virtual int userId { get; set; }
        public virtual User user { get; set; }
    }
}