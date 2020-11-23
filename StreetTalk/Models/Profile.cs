using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreetTalk.Models
{
    public class Profile : Timestamped
    {
        public virtual int id { get; set; }
        
        [StringLength(45)]
        public virtual string? firstName { get; set; }
        
        [StringLength(45)]
        public virtual string? lastName { get; set; }
        
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public virtual DateTime? dateOfBirth { get; set; }
        
        [StringLength(64)]
        public virtual string? city { get; set; }
        
        [StringLength(64)]
        public virtual string? street { get; set; }
        
        public virtual int? houseNumber { get; set; }
        
        [StringLength(5)]
        public virtual string? houseNumberAddition { get; set; }
        
        public virtual int userId { get; set; }
        public virtual User user { get; set; }
    }
}