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

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            
            return RedirectToAction("Registreren");
        }

        public IActionResult TryLogin(User user)
        {
            if (user.email.ToLower() == "streettalk@gmail.com" && user.password == "12345" /*Check whether Username and Password are correct, use Db in future*/)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Login");
        }
    }
}