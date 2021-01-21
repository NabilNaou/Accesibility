using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace StreetTalk.Models
{
    public class PublicPost : Timestamped
    {
        public virtual int Id { get; set; }
        
        [StringLength(64)]
        [DisplayName("Titel")]
        [MinLength(3, ErrorMessage = "Titel bevat minder dan 3 letters")]
        [Required(ErrorMessage = "Dit veld is verplicht")]
        public virtual string Title { get; set; }
        
        [Column(TypeName = "text")]
        [DisplayName("Inhoud")]
        [MinLength(10, ErrorMessage = "Inhoud bevat minder dan 10 letters")]
        [Required(ErrorMessage = "Dit veld is verplicht")]
        public virtual string Content { get; set; }
        
        public virtual PostPhoto Photo { get; set; }
        
        public virtual string UserId { get; set; }
        public virtual StreetTalkUser User { get; set; }
        
        [NotMapped]
        [DataType(DataType.Upload)]
        [DisplayName("Foto")]
        public virtual IFormFile UploadedPhoto { get; set; }
        
        [NotMapped]
        [DisplayName("Foto is aanstootgevend")]
        public virtual bool UploadedPhotoIsSensitive { get; set; }
        
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
