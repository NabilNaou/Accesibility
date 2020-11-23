using System.ComponentModel.DataAnnotations;

namespace StreetTalk.Models
{
    public class AnonymousPost : Post
    {
        [StringLength(64)]
        public string pseudonym { get; set; }
    }
}