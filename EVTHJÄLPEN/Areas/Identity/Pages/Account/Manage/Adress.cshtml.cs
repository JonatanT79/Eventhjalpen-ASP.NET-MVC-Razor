using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eventhjälpen.Models;
using EVTHJÄLPEN.Data;
using EVTHJÄLPEN.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace EVTHJÄLPEN.Areas.Identity.Pages.Account.Manage
{
    public class AdressModel : PageModel
    {

        public int ID { get; set; }
        public string Street { get; set; }
        public int ZipCode { get; set; }
        public string CareOf { get; set; }
        public string City { get; set; }
        public Guid UserID { get; set; }

        public List<AdressModel> adressList = new List<AdressModel>();
        public bool isAdressEmpty;

        public IActionResult OnGet()
        {

            var signedInUserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                // If userID don't excist site will not load any userdata
                var getAdressinfo = from a in ctx.UserAdress
                                    where a.UserID == Guid.Parse(signedInUserID)
                                    select a;

                foreach (var item in getAdressinfo)
                {
                    AdressModel adress = new AdressModel();

                    adress.ID = item.ID;
                    adress.Street = item.Adress;
                    adress.ZipCode = Convert.ToInt32(item.ZipCode);
                    adress.City = item.City;
                    adress.CareOf = item.CareOf;
                    adress.UserID = item.UserID;
                    adressList.Add(adress);
                }
                return Page();
            }
        }
        public IActionResult OnPost(string Adress, string CareOf, int ZipCode, string City)
        {

            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                AdressModel adressModel = new AdressModel();
                var signedInUserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();

                var checkEmptyAdress = from e in ctx.UserAdress
                                       select e;
                if (checkEmptyAdress.Count() == 0)
                {
                    isAdressEmpty = true;
                }

                if (isAdressEmpty == true)
                {
                    var query = from user in ctx.UserAdress
                                where user.UserID == Guid.Parse(signedInUserID)
                                select user;

                    UserAdress ua = new UserAdress();
                    ua.Adress = Adress;
                    ua.CareOf = CareOf;
                    ua.ZipCode = ZipCode.ToString();
                    ua.City = City;
                    ua.UserID = Guid.Parse(signedInUserID);
                    ctx.UserAdress.Add(ua);
                    ctx.SaveChanges();
                }
                else
                {
                    var query = from user in ctx.UserAdress
                                where user.UserID == Guid.Parse(signedInUserID)
                                select user;

                    foreach (var adress in query)
                    {
                        adress.Adress = Adress;
                        adress.CareOf = CareOf;
                        adress.ZipCode = ZipCode.ToString();
                        adress.City = City;
                        adress.UserID = Guid.Parse(signedInUserID);
                    }
                    ctx.SaveChanges();
                }


            }
            return Page();
        }
    }
}

