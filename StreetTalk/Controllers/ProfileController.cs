using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StreetTalk.Data;
using StreetTalk.Models;
using StreetTalk.Services;

namespace StreetTalk.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly UserService userService;
        private readonly StreetTalkSignInManager signInManager;

        public ProfileController(StreetTalkContext context, UserService userService, StreetTalkSignInManager signInManager) : base(context)
        {
            this.userService = userService;
            this.signInManager = signInManager;
        }

        public IActionResult Index(bool editSuccess = false)
        {
            var user = userService.GetCurrentlyLoggedInUser();
            if (user == null) return BadRequest("User not logged in");

            ViewData["editSuccess"] = editSuccess;
            
            return View(user.Profile);
        }
        
        public IActionResult DeleteAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(string email)
        {
            var user = userService.GetCurrentlyLoggedInUser();
            if (user == null) return BadRequest("User not logged in");
            if (user.Email != email) return BadRequest("Emails do not match");
            
            Db.Remove(user.Profile);
            
            user.Profile = null;
            user.Email = null;
            user.PasswordHash = null;
            user.LastKnownIpAddress = null;
            user.UserName = null;
            user.NormalizedEmail = null;
            user.NormalizedUserName = null;

            await Db.SaveChangesAsync();
            
            await signInManager.SignOutAsync(); 

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult FetchPersonalData()
        {
            var user = userService.GetCurrentlyLoggedInUser();
            if (user == null) return BadRequest("User not logged in");

            var personalData = new Dictionary<string, string>
            {
                {"Email", user.Email},
                {"Full name", user.Profile.FullName},
            };

            //Optional fields
            if (user.Profile.DateOfBirth != null)
                personalData.Add("DateOfBirth", user.Profile.DateOfBirth.Value.ToShortDateString());
            
            if (user.Profile.City != null)
                personalData.Add("City", user.Profile.City);

            if (user.Profile.Street != null)
                personalData.Add("Street", user.Profile.Street);

            if (user.Profile.HouseNumber != null)
                personalData.Add("HouseNumber", user.Profile.HouseNumber.ToString());

            if (user.Profile.HouseNumberAddition != null)
                personalData.Add("HouseNumberAddition", user.Profile.HouseNumberAddition);
            
            if (user.Profile.PostalCode != null)
                personalData.Add("PostalCode", user.Profile.PostalCode);
            
            if (user.LastKnownIpAddress != null)
                personalData.Add("LastKnownIpAddress", user.LastKnownIpAddress);

            //Download as json
            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(
                JsonSerializer.SerializeToUtf8Bytes(personalData, new JsonSerializerOptions {WriteIndented = true}),
                "application/json");
        }

        [HttpPost]
        public IActionResult Index(Profile profile)
        {
            var user = userService.GetCurrentlyLoggedInUser();
            if (user == null) return BadRequest("User not logged in");

            if (!ModelState.IsValid)
            {
                return View(profile);
            }

            user.Profile.City = profile.City;
            user.Profile.Street = profile.Street;
            user.Profile.HouseNumber = profile.HouseNumber;
            user.Profile.HouseNumberAddition = profile.HouseNumberAddition;
            user.Profile.FirstName = profile.FirstName;
            user.Profile.LastName = profile.LastName;
            user.Profile.DateOfBirth = profile.DateOfBirth;
            user.Profile.PostalCode = profile.PostalCode;
            
            Db.SaveChanges();

            return RedirectToAction("Index", new { editSuccess = true });
        }
    }
}