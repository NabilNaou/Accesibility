namespace StreetTalk.Models
{
    public class Photo
    {
        public virtual int Id { get; set; }
        
        public virtual string Filename { get; set; }
        
        public virtual bool Sensitive { get; set; }
        
        public virtual Post Post { get; set; }
    }
}