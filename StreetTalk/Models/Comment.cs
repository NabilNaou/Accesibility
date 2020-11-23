using System.ComponentModel.DataAnnotations;

namespace StreetTalk.Models
{
    public class Comment : Timestamped
    {
        public int id { get; set; }
        
        [StringLength(600)]
        public string content { get; set; }
    }
}