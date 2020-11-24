using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreetTalk.Models
{
    public class User : Timestamped
    {
        public virtual int id { get; set; }
        
        [Display(Name = "Email adres")]
        public virtual string email { get; set; }
        
        public virtual bool emailConfirmed { get; set; }
        
        public virtual string passwordHash { get; set; }
        
        [NotMapped]
        [Display(Name = "Wachtwoord")]
        public virtual string password { get; set; }
        
        [NotMapped]
        [Display(Name = "Wachtwoord herhaald")]
        public virtual string confirmPassword { get; set; }

        public virtual DateTime? lockoutEndTime { get; set; }
        
        public virtual bool lockoutEnabled { get; set; }
        
        public virtual int accessFailedCount { get; set; }
        
        public virtual Profile profile { get; set; }
        
        public virtual List<Comment> comments { get; } = new List<Comment>();
        
        public virtual List<Like> likes { get; } = new List<Like>();
        
        public virtual List<Post> posts { get; } = new List<Post>();
    }
}