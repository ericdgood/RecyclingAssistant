using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
                        var emailMessage = item.BodyParts.OfType<TextPart>().FirstOrDefault()?.Text;

                        string concat = string.Join("\n ", ReturnReceiptItems(emailMessage).ToArray());
                        return GetUserZipCode(emailMessage) + "\n" + concat + emailMessage;
                    }
                }
                return "";
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
