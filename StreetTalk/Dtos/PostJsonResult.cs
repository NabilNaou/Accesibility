using System;

namespace StreetTalk.Dtos
{
    [Serializable]
    public class PostJsonResult
    {
        public bool Succes { get; set; }
        public string Error { get; set; } = "";
        public int NewLikes { get; set; }
    }
}