using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Linq;
using System.Threading.Tasks;
using Eventhjälpen.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Data.SqlClient;

namespace EVTHJÄLPEN.Areas.Identity.Pages.Account.Manage
{
    public partial class EmailModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public EmailModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "New email")]
            public string NewEmail { get; set; }
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var email = await _userManager.GetEmailAsync(user);
            Email = email;

            Input = new InputModel
            {
                NewEmail = email,
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Kan inte ladda användare med id '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Kan inte ladda användare med id '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var email = await _userManager.GetEmailAsync(user);
            if (Input.NewEmail != email)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateChangeEmailTokenAsync(user, Input.NewEmail);
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { userId = userId, email = Input.NewEmail, code = code },
                    protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(
                    Input.NewEmail,
                    "Bekräfta din e-postadress",
                    $"Bekräfta ditt konto genom att <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>klicka här.</a>.");

                // insert/update in database
                var emails = await _userManager.GetEmailAsync(user);

                if (Input.NewEmail != emails)
                {
                    var setEmailResult = await _userManager.SetEmailAsync(user, Input.NewEmail);
                    var setnewUserNameResult = await _userManager.SetUserNameAsync(user, Input.NewEmail);

                    using (SqlConnection con = new SqlConnection("Server=(localdb)\\Mssqllocaldb; Database= TranbarDB; MultipleActiveResultSets=true"))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(@"update[TranbarDB].[dbo].[AspNetUsers]
                                                            set EmailConfirmed = 1
                                                            where Email = @Email", con);

                        cmd.Parameters.AddWithValue("@Email", Input.NewEmail);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                        if (!setEmailResult.Succeeded)
                        {
                            var userIds = await _userManager.GetUserIdAsync(user);
                            throw new InvalidOperationException($"Kunde inte uppdatera lösenord för användare med id '{userIds}'.");
                        }
                }

                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = "E-postadress uppdaterad!";
                return RedirectToPage();
            }

            StatusMessage = "Kunde inte uppdatera e-postadress.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Kan inte ladda användare med id '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                "Bekräfta din e-postadress",
                $"Bekräfta ditt konto genom att <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>klicka här</a>.");

            StatusMessage = "Bekräftelse har skickats till din e-postadress.";
            return RedirectToPage();
        }
    }
}
