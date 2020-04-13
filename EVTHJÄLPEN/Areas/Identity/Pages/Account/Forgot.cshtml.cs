using System;
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
    public class ForgotModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ForgotModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "E-postadress")]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var users = _userManager.Users;

            var getAllEmails = from u in users
                               select u.Email;

            if (getAllEmails.Contains(Input.Email))
            {
                EmailSender es = new EmailSender();
                returnUrl = returnUrl ?? Url.Content("~/");
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    var result = await _userManager.GetUserIdAsync(user);
                    if (result != null)
                    {
                        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var callbackUrl = Url.Page(
                            "/Account/Reset",
                            pageHandler: null,
                            values: new { code },
                            protocol: Request.Scheme);

                        Email em = new Email()
                        {
                            to = Input.Email,
                            subj = "Glömt lösenord",
                            body = $"<h1>Har du glömt ditt lösenord?</h1><br>Klicka <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>här</a> för att återställa ditt lösenord."
                        };

                        es.SendEmail(em);
                    }
                }
            }
            else
            {
                return Page();
            }
            return Page();
        }
    }
}

