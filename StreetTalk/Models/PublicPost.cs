using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace StreetTalk.Models
{
    public class PublicPost : Post
    {
        public virtual bool Closed { get; set; }
        
        [DisplayName("Categorie")]
        public virtual int CategoryId { get; set; }
        public virtual PostCategory Category { get; set; }

        public virtual List<Comment> Comments { get; } = new List<Comment>();
        
        public virtual List<Like> Likes { get; } = new List<Like>();
        
        public virtual List<Report> Reports { get; } = new List<Report>();

        public virtual List<View> Views { get; } = new List<View>();

        public DateTime? GetMostRecentCommentDate()
        {
            return Comments.OrderByDescending(t => t.CreatedAt).Select(p => p.CreatedAt).FirstOrDefault();
        }

        public bool IsClosed()
        {
            var recentComment = GetMostRecentCommentDate();
            if (recentComment == null)
                return Closed;

            var currentTime = DateTime.Now;
            var difference = (currentTime - recentComment!.Value).TotalDays;
            var exceedDate = difference >= 30;

            return Closed || exceedDate;
        }
    }
}
