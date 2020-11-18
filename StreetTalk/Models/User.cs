using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreetTalk.Models
{
    public class User
    {
        public int? id { get; set; }
        
        [Display(Name = "Email adres")]
        public string email { get; set; }
        
        [Display(Name = "Wachtwoord")]
        public string password { get; set; }
        
        [NotMapped]
        [Display(Name = "Wachtwoord herhaald")]
        public string confirmPassword { get; set; }
    }
}