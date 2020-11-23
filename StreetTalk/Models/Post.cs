using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreetTalk.Models
{
    public class Post : Timestamped
    {
        public int id { get; set; }
        
        [StringLength(64)]
        public string title { get; set; }
        
        [Column(TypeName = "text")]
        public string content { get; set; }
        
        public int? photoId { get; set; }
        public Photo photo { get; set; }
    }
}