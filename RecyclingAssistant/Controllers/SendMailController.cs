using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecyclingAssistant.MailServices;

namespace RecyclingAssistant.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private readonly MailSender _sender;

        public SendMailController(MailSender sender)
        {
           _sender = sender;
        }

        [HttpGet]
        public ActionResult SendTest()
        {
            _sender.TestSendAsync().Wait();

            return Ok();
        }
    }
}