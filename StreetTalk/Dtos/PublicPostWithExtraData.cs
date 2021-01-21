using StreetTalk.Models;

namespace StreetTalk.Dtos
{
    public class PublicPostWithExtraData
    {
        public PublicPost Post { get; set; }
        public bool Liked { get; set; }

        public bool Reported { get; set; }
    }
}