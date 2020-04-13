using Eventhjälpen.Models;
using EVTHJÄLPEN.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EVTHJÄLPEN.Components
{
    public class EventMenu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            List<Events> eventList = new List<Events>();
            using (ApplicationDbContext ctx = new ApplicationDbContext())
            {
                eventList = ctx.Events
                      .ToList();
            }

            var events = eventList;
            return View(events);
        }
    }
}

