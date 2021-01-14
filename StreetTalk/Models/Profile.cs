#nullable enable
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StreetTalk.Models
{
    public class Profile : Timestamped
    {
        public virtual int Id { get; set; }
        
        [Encrypted]
        [DisplayName("Voornaam")]
        public virtual string? FirstName { get; set; }
        
        [Encrypted]
        [DisplayName("Achternaam")]
        public virtual string? LastName { get; set; }

        [NotMapped]
        [ValidateNever]
        public virtual string FullName
        {
            get
            {
                //TODO: Find an alternative for this, privacy concern.
                var name = User.Email;
                
                if (!FirstName.IsNullOrEmpty() && !LastName.IsNullOrEmpty())
                    name = $"{FirstName} {LastName}";
                else if (!FirstName.IsNullOrEmpty())
                    name = FirstName!;

                return name;
            }
        }

        [DataType(DataType.Date)]
        [Column(TypeName = "Date")]
        [Encrypted]
        [DisplayName("Geboortedatum")]
        public virtual DateTime? DateOfBirth { get; set; }
        
        [Encrypted]
        [DisplayName("Plaats")]
        public virtual string? City { get; set; }
        
        [Encrypted]
        [DisplayName("Straatnaam")]
        public virtual string? Street { get; set; }
        
        [Encrypted]
        [DisplayName("Huisnummer")]
        public virtual int? HouseNumber { get; set; }
        
        [Encrypted]
        [DisplayName("Huisnummer toevoeging")]
        public virtual string? HouseNumberAddition { get; set; }
        
        [Encrypted]
        [DisplayName("Postcode")]
        public virtual string? PostalCode { get; set; }
        
        [ValidateNever]
        public virtual string UserId { get; set; }
        [ValidateNever]
        public virtual StreetTalkUser User { get; set; }
        
        [ValidateNever]
        public virtual ProfilePhoto Photo { get; set; }
    }
}