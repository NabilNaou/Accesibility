using System.Collections.Generic;

namespace StreetTalk.Dtos
{
    public class PublicPostViewModel
    {
        public List<PublicPostWithExtraData> Posts { get; set; }
        public PublicPostListFilters Filters { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }
    }
}