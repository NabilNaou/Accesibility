using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreetTalk.Models
{
    public class User : Timestamped
    {
        public virtual int Id { get; set; }
        
        [Display(Name = "Email adres")]
        public virtual string Email { get; set; }
        
        public virtual bool EmailConfirmed { get; set; }
        
        public virtual string PasswordHash { get; set; }
        
        [NotMapped]
        [Display(Name = "Wachtwoord")]
        public virtual string Password { get; set; }
        
        [NotMapped]
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