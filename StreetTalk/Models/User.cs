using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreetTalk.Models
{
    public class User
    {
        public int? id { get; set; }
        
        [EmailAddress(ErrorMessage="Email bestaat niet")]
        [Required(ErrorMessage="Email moet worden ingevuld")]
        [Display(Name = "email")]
        public string email { get; set; }
        
        [Required(ErrorMessage="Wachtwoord is verplicht")]
        [Display(Name = "password")]
        public string password { get; set; }
        
        [Compare("password", ErrorMessage="Wachtwoord komt niet overeen")]
        [Display(Name = "confirmPassword")]
        public string confirmPassword { get; set; }
    }
}