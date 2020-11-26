using Microsoft.AspNetCore.Mvc;
using StreetTalk.Models;

namespace StreetTalk.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(StreetTalkContext context) : base(context) {}
        
        public IActionResult Index()
        {
            return RedirectToAction("Registreren");
        }
        
        public IActionResult Login()
        {
            return View();
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

            user.Profile = new Profile();
            
            Db.User.Add(user);
            Db.SaveChanges();
            
            return RedirectToAction("VerifieerEmail");
        }

        public IActionResult VerifieerEmail(string code)
        {
            //TODO: Als er geen gebruiker is ingelogt, redirect naar login

            if (code == null)
                return View();

            //TODO: Verifieer code met de code in de database
            
            return View("VerifieerEmailSucess");;
        }
    }
}