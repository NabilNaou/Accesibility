using Microsoft.AspNetCore.Mvc;
using StreetTalk.Models;

namespace StreetTalk.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Registreren");
        }
        
        public IActionResult Registreren()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registreren(User user)
        {
            if(!ModelState.IsValid)
                return View(user);
            
            //TODO: Add user to database
            return RedirectToAction("Registreren");
        }
    }
}