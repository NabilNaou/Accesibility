using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreetTalk.Models
{
    public class User : Timestamped
    {
        public virtual int Id { get; set; }
        
        [EmailAddress(ErrorMessage="Ongeldig email adres")]
        [Required(ErrorMessage="Email moet worden ingevuld")]
        [Display(Name = "Email adres")]
        public virtual string Email { get; set; }
        
        public virtual bool EmailConfirmed { get; set; }
        
        public virtual string PasswordHash { get; set; }
        
        [NotMapped]
        [Required(ErrorMessage="Wachtwoord is verplicht")]
        [Display(Name = "Wachtwoord")]
        public virtual string Password { get; set; }
        
        [NotMapped]
        [Compare("Password", ErrorMessage="Wachtwoord komt niet overeen")]
        [Display(Name = "Wachtwoord herhaald")]
        public virtual string ConfirmPassword { get; set; }

        public virtual DateTime? LockoutEndTime { get; set; }
        
        public virtual bool LockoutEnabled { get; set; }
        
        public virtual int AccessFailedCount { get; set; }
        
        public virtual Profile Profile { get; set; }
        
        public virtual List<Comment> Comments { get; } = new List<Comment>();
        
        public virtual List<Like> Likes { get; } = new List<Like>();
        
        public virtual List<Post> Posts { get; } = new List<Post>();
    }
}