using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StreetTalk.Models;

namespace StreetTalk.Models
{
    // Add profile data for application users by adding properties to the StreetTalkUser class
    public class StreetTalkUser : IdentityUser
    {
        public virtual Profile Profile { get; set; }

        public virtual List<Comment> Comments { get; } = new List<Comment>();

        public virtual List<Like> Likes { get; } = new List<Like>();

        public virtual List<Post> Posts { get; } = new List<Post>();
    }
}
