using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StreetTalk.Dtos
{
    public class PublicPostListFilters
    {
        public bool ShowClosedPosts { get; set; }
        public string SorteerOptie { get; set; }
        public bool OnlyLikedPosts { get; set; }
        public string ZoekFilter { get; set; }

        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartTime { get; set; }
        
        [DisplayName("End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndTime { get; set; }
    }
}