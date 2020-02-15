using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecyclingAssistant.MailServices;

namespace RecyclingAssistant.Controllers
{
    [Route("api/[controller]")]
    public class GetMailController : Controller
    {
        private readonly GetMail _getMail;

        public GetMailController(GetMail getMail)
        {
            _getMail = getMail;
        }

        // GET: api/values
        [HttpGet]
        public ActionResult<string> GetMail()
        {
            return Ok(_getMail.GetHelpMeMail());
        }

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
