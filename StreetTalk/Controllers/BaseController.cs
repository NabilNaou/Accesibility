using Microsoft.AspNetCore.Mvc;

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