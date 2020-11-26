namespace StreetTalk.Models
{
    public class ProfilePhoto
    {
        public virtual int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
        
        public virtual int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}