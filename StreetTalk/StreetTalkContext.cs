using Microsoft.EntityFrameworkCore;
using StreetTalk.Models;

namespace StreetTalk
{
    public class StreetTalkContext : DbContext
    {
        
        public StreetTalkContext(DbContextOptions options) : base(options) {}

    }
}