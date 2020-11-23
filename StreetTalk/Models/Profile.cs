using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StreetTalk.Models
{
    public class Profile : Timestamped
    {
        public int id { get; set; }
        
        [StringLength(45)]
        public string? firstName { get; set; }
        
        [StringLength(45)]
        public string? lastName { get; set; }
        
        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        public DateTime? dateOfBirth { get; set; }
        
        [StringLength(64)]
        public string? city { get; set; }
        
        [StringLength(64)]
        public string? street { get; set; }
        
        public int? houseNumber { get; set; }
        
        [StringLength(5)]
        public string? houseNumberAddition { get; set; }
        
        public int userId { get; set; }
        public User user { get; set; }
    }
}