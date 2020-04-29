using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EVTHJÄLPEN.Models;
using Eventhjälpen.Models;
using EVTHJÄLPEN.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EVTHJÄLPEN.Controllers
{
    [ApiController]
    public class RecipeAPIController : ControllerBase
    {
        [HttpGet("api/Recipe/{id}")]
        public ActionResult<string> Get(int id)
        {
            var result = new APIFormattedRecipe(id);
            if (result != null && id < 7)
            {
                string json = JsonConvert.SerializeObject(result, Formatting.None);
                json = JsonConvert.SerializeObject(result, Formatting.Indented);
                return json;
            }
            else 
            {
                return "Receptet med det ID finns inte";
            }
            
        }
    }
}