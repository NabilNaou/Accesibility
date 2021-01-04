using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace StreetTalk.Models
{
    public class Post : Timestamped
    {
        public virtual int Id { get; set; }
        
        [StringLength(64)]
        public virtual string Title { get; set; }
        
        [Column(TypeName = "text")]
        public virtual string Content { get; set; }
        
        public virtual PostPhoto Photo { get; set; }
        
        public virtual string UserId { get; set; }
        public virtual StreetTalkUser User { get; set; }
        
        [NotMapped]
        [DataType(DataType.Upload)]
        public virtual IFormFile UploadedPhoto { get; set; }
        [NotMapped]
        public virtual bool UploadedPhotoIsSensitive { get; set; }
    }
}