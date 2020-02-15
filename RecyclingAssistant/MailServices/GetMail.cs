using System;
using System.Collections.Generic;
using System.Linq;
using MailKit;
using MailKit.Net.Imap;
using MimeKit;

namespace RecyclingAssistant.MailServices
{
    public class GetMail
    {
        public GetMail()
        {
        }

        public virtual string GetHelpMeMail()
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

                    foreach (var item in msgs)
                    {
                        return item.BodyParts.OfType<TextPart>().FirstOrDefault()?.Text;
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
