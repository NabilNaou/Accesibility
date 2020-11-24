using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreetTalk.Models
{
    public class Profile : Timestamped
    {
        public virtual int Id { get; set; }
        
        [StringLength(45)]
        public virtual string? FirstName { get; set; }
        
        [StringLength(45)]
        public virtual string? LastName { get; set; }
        
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public virtual DateTime? DateOfBirth { get; set; }
        
        [StringLength(64)]
        public virtual string? City { get; set; }
        
        [StringLength(64)]
        public virtual string? Street { get; set; }
        
        public virtual int? HouseNumber { get; set; }
        
        [StringLength(5)]
        public virtual string? HouseNumberAddition { get; set; }
        
        public virtual int UserId { get; set; }
        public virtual User User { get; set; }
    }
}