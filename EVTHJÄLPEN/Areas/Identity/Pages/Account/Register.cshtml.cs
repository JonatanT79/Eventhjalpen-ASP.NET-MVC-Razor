﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Eventhjälpen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using EVTHJÄLPEN.Models;
using EVTHJÄLPEN.Services;

namespace EVTHJÄLPEN.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "E-postadress")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "{0} måste vara minst {2} och som max {1} tecken långt.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Bekräfta lösenord")]
            [Compare("Password", ErrorMessage = "Lösenorden stämmer inte överens.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            EmailSender es = new EmailSender();
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email};
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Användare med nytt lösenord skapad.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    //Här är välkomstmailet, baseras på en modell i Models. To är vem som skall få den, Subj är titel på mail, Body är innehåll. 
                    //HTML formattering fungerar på detta. 
                    Email em = new Email()
                    {
                        to = Input.Email,
                        subj = "Välkommen till Eventhjälpen!",
                        body = $"<h1>Välkommen!</h1><br>Bekräfta mailadress genom att <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>klicka här!</a>." +
                        $"<br>" +
                        $"<br>Hej och välkommen till Eventhjälpen, vad kul att du har blivit vår nya kund!" +
                        $"<br>" +
                        $"<br>Eventhjälpen skapades år 2020 och vår affärsidé är underlätta din planering inför ditt event. På vår hemsida kan du filtrera dig fram till det event du har tänkt skapa, samtidigt som du kan välja recept och få ingredienser hemlevererat. Vår hemsida går ut på att vara enkel och smidig!" +
                        $"<br>" + 
                        $"<br>Har du ytterligare frågor eller undrar över något? " +
                        $"<br>" +
                        $"<br>Vi finns tillgänglig på vardagar mellan klockan 09-17, övriga tider nås vi via mejl. Observera att vi har en svarstid på upp till tre arbetsdagar. " +
                        $"<br>" +
                        $"Våra kontaktuppgifter är följande:" +
                        $"<br>Mail: evthjalpen@gmail.com" +
                        $"<br>Telefon: 08-123 45 67" +
                        $"<br>" +
                        $"<br>Lycka till med ditt event! " +
                        $"<br>Vänliga hälsningar, Eventhjälpen"
                    };

                    es.SendEmail(em);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

                // If we got this far, something failed, redisplay form
                return Page();
        }
    }
}

