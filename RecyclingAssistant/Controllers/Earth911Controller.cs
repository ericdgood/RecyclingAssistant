using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecyclingAssistant.Earth911Servies;

namespace RecyclingAssistant.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Earth911Controller : ControllerBase
    {
        private readonly E911Service _e911Serivce;

        public Earth911Controller(E911Service E911Serivce)
        {
            _e911Serivce = E911Serivce;
        }

        [HttpGet("{zipCode}")]
        public async Task<ActionResult> GetZipDetails(string zipCode)
        {
            var location = await _e911Serivce.GetZipMaterialsAndLocations(zipCode);

            return Ok(location);
        }
    }
}