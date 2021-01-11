using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace StreetTalk.Models
{
    public class StreetTalkUser : IdentityUser
    {
        public virtual string LastKnownIpAddress { get; set; }
        public virtual Profile Profile { get; set; }

        public virtual List<Comment> Comments { get; } = new List<Comment>();

        public virtual List<Like> Likes { get; } = new List<Like>();

        public virtual List<Post> Posts { get; } = new List<Post>();
    }
}
