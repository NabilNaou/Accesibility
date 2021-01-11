#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Castle.Core.Internal;

namespace StreetTalk.Models
{
    public class Profile : Timestamped
    {
        public virtual int Id { get; set; }
        
        [Encrypted]
        public virtual string? FirstName { get; set; }
        
        [Encrypted]
        public virtual string? LastName { get; set; }

        [NotMapped]
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
        public virtual DateTime? DateOfBirth { get; set; }
        
        [Encrypted]
        public virtual string? City { get; set; }
        
        [Encrypted]
        public virtual string? Street { get; set; }
        
        [Encrypted]
        public virtual int? HouseNumber { get; set; }
        
        [Encrypted]
        public virtual string? HouseNumberAddition { get; set; }
        
        public virtual string UserId { get; set; }
        public virtual StreetTalkUser User { get; set; }
        
        public virtual ProfilePhoto Photo { get; set; }
    }
}