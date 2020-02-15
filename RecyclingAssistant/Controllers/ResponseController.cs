using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecyclingAssistant.Earth911Servies;
using RecyclingAssistant.MailServices;

namespace RecyclingAssistant.Controllers
{

    public class ResponseController : Controller
    {
        private readonly E911Service _e911Service;
        private readonly GetMail _mailService;

        public ResponseController(E911Service e911Service, GetMail mailService)
        {
            _e911Service = e911Service;
            _mailService = mailService;
        }

        public async Task<IActionResult> Index()
        {

            var mail = await _mailService.GetHelpMeMailAsync();

            var viewData = new List<ViewModel>();

            var index = 0;


            foreach (var item in mail.Programs)
            {
                var data = new ViewModel();
                data.Program = item;

                if (mail.ReceiptItems.Count() >= 2)
                {
                    data.ReceiptItems.AddRange(mail.ReceiptItems.Skip(index).Take(2));
                    index = index + 2;

                    viewData.Add(data);
                }
                else
                {
                    break;
                }
            }

            return View(viewData);
        }
    }
}