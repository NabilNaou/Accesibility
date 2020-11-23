using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreetTalk.Models
{
    public class User
    {
        public int id { get; set; }
        
        [Display(Name = "Email adres")]
        public string email { get; set; }
        
        public bool emailConfirmed { get; set; }
        
        [Display(Name = "Wachtwoord")]
        public string passwordHash { get; set; }
        
        [NotMapped]
        [Display(Name = "Wachtwoord herhaald")]
        public string confirmPassword { get; set; }
        
        public DateTime? createdAt { get; set; }
        
        public DateTime? modifiedAt { get; set; }
        
        public DateTime? lockoutEndTime { get; set; }
        
        public bool lockoutEnabled { get; set; }
        
        public int accessFailedCount { get; set; }

        public User()
        {
            createdAt = DateTime.UtcNow;
            modifiedAt = DateTime.UtcNow;
        }
    }
}