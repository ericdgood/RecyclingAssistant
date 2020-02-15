using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;
using RecyclingAssistant.Controllers;
using RecyclingAssistant.Earth911Servies;
using RecyclingAssistant.Earth911Servies.models;
using RecyclingAssistant.MailServices.model;

namespace RecyclingAssistant.MailServices
{
    public class GetMail
    {
        private E911Service _earth911service;

        public GetMail()
        {
            _earth911service = new E911Service(new HttpClient());
        }

        public async Task<DisplayObject> GetHelpMeMailAsync()
        {

            try
            {
                using (var client = new ImapClient())
                {
                    client.Connect("outlook.office365.com", 993, true);
                    client.Authenticate(
                        "helpme@Cyber-Medics.com",
                        @"*Jk-cq3""sKr6T]\h"
                        );

                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);
                    var strOut = new List<string>();
                    strOut.Add("Total Messages: " + inbox.Count);
                    strOut.Add("Messages follow:");
                    var msgs = new List<MimeMessage>();
                    for (int i = 0; i < inbox.Count; i++)
                    {
                        msgs.Add(inbox.GetMessage(i));
                    }

                    client.Disconnect(true);

                    foreach (var email in msgs)
                    {
                        var emailMessage = email.BodyParts.OfType<TextPart>().FirstOrDefault()?.Text;
                        List<RecycProgramDetails> programs = new List<RecycProgramDetails>();

                        var receiptItems = ReturnReceiptItems(emailMessage);
                        var zip = GetUserZipCode(emailMessage);
                        var zipResults = await _earth911service.GetZipLocationDetails(zip);

                        foreach (var result in zipResults.result)
                        {
                            programs.Add(result.Value);
                        }

                        return new DisplayObject()
                        {
                            Programs = programs,
                            ReceiptItems = receiptItems
                        };
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<string> ReturnReceiptItems(string emailMessage)
        {
            List<string> receiptItems = new List<string>();
            string[] lines = emailMessage.Split(
            new[] { Environment.NewLine },
            StringSplitOptions.None);

            foreach (var line in lines)
            {
                if (line != null)
                {
                    if (line.Length > 4)
                    {
                        string str = line?.Substring(0, 4);

                        char[] charArray = str.ToCharArray();
                        if (char.IsUpper(charArray[2]) && char.IsUpper(charArray[3]))
                        {
                            receiptItems.Add(line);
                        }
                    }
                }
            }
            return receiptItems;
        }

        private string GetUserZipCode(string emailMessage)
        {
            Regex regex = new Regex(@"\d{5}");

            Match match = regex.Match(emailMessage);

            if (match.Success)
            {
                return match.Value.ToString();
            }
            return null;
        }

    }
}
