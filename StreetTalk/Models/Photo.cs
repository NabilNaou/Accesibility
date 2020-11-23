namespace StreetTalk.Models
{
    public class Photo
    {
        public virtual int id { get; set; }
        
        public virtual string filename { get; set; }
        
        public virtual bool sensitive { get; set; }
        
        public virtual Post post { get; set; }
    }
}