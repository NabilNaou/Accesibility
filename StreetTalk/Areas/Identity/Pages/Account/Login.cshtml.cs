using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using StreetTalk.Models;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Http;
using StreetTalk.Data;

namespace StreetTalk.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<StreetTalkUser> _userManager;
        private readonly SignInManager<StreetTalkUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ICaptchaValidator _captchaValidator;
        private readonly HttpContext _httpContext;
        private readonly StreetTalkContext _context;
        private readonly IEmailSender _emailSender;

        public LoginModel(SignInManager<StreetTalkUser> signInManager, 
            ILogger<LoginModel> logger,
            UserManager<StreetTalkUser> userManager,
            ICaptchaValidator captchaValidator,
            IHttpContextAccessor httpContextAccessor,
            StreetTalkContext context,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _captchaValidator = captchaValidator;
            _httpContext = httpContextAccessor.HttpContext;
            _context = context;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string captcha, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (!await _captchaValidator.IsCaptchaPassedAsync(captcha))
            {
                ModelState.AddModelError("captcha", "Captcha validation failed");
                return Page();
            }

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    //Notify user about login from a different ip address
                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    if (user != null)
                    {
                        var ip = _httpContext.Connection.RemoteIpAddress.ToString();
                        if (user.LastKnownIpAddress != ip)
                        {
                            //Logged in from new ip address
                            user.LastKnownIpAddress = ip;
                            await _context.SaveChangesAsync();
                            _logger.LogInformation("User logged in from new ip address");

                            await _emailSender.SendEmailAsync(user.Email, "Login vanaf een nieuwe locatie", $"Er is inglogt op uw account vanaf een nieuwe locatie ({ip}).<br>Als u dit niet was, raden wij aan om uw wachtwoord te veranderen en 2 factor authenticatie in te stellen.");
                        }
                    }
                    
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ongeldige login");

                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    if (user == null) return Page();
                    
                    var failedAttempts = await _userManager.GetAccessFailedCountAsync(user);
                    if (failedAttempts >= 3)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Als u uw wachtwoord vergeten bent, klik op 'Forgot your password?'");
                    }

                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
