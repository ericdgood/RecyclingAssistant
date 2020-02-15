using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecyclingAssistant.Earth911Servies;

namespace RecyclingAssistant.Controllers
{
    public class ResponseController : Controller
    {
        private readonly E911Service _e911Service;

        public ResponseController(E911Service e911Service )
        {
            _e911Service = e911Service;
        }

        public async Task<IActionResult> IndexAsync()
        {

            var data = await _e911Service.GetZipLocationDetails("32779");

            var newData = data.result.Select(d => d.Value).ToList();

            return View(newData);
        }
    }
}