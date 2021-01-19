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

        public virtual List<PublicPost> Posts { get; } = new List<PublicPost>();

        public string GetDisplayName()
        {
            return Profile == null ? "[verwijderde gebruiker]" : Profile.FullName;
        }

        public string GetProfilePhoto()
        {
            return Profile == null ? "/img/profile_photo_placeholder.png" : Profile.Photo.Photo.Filename;
        }
    }
}
