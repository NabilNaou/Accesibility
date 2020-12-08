using Microsoft.AspNetCore.Mvc;
using StreetTalk.Data;

namespace StreetTalk.Controllers
{
    public class BaseController : Controller
    {
        protected readonly StreetTalkContext Db;
        
        public BaseController(StreetTalkContext context)
        {
            Db = context;
        }
    }
}