using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace StreetTalk.Models
{
    public class AnonymousPost : Timestamped
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

        [NotMapped]
        [DataType(DataType.Upload)]
        [DisplayName("Foto")]
        public virtual IFormFile UploadedPhoto { get; set; }
        
        [NotMapped]
        [DisplayName("Foto is aanstootgevend")]
        public virtual bool UploadedPhotoIsSensitive { get; set; }
        
        [StringLength(64)]
        public string Pseudonym { get; set; }
    }
}