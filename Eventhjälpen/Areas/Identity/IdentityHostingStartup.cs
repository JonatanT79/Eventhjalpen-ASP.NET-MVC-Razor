using System;
using Eventhjalpen.Data;
using Eventhjälpen.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Eventhjälpen.Areas.Identity.IdentityHostingStartup))]
namespace Eventhjälpen.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<EventhjalpenContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("EventhjalpenContextConnection")));

                services.AddDefaultIdentity<EventhjalpenUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<EventhjalpenContext>();
            });
        }
    }
}