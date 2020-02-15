using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecyclingAssistant.MailServices
{
    public class MailSender
    {
        public async Task<MimeMessage> SendAsync(List<string> TOs, string Subject, string Body, List<string> CCs = null, List<string> BCCs = null, string AttachBase64 = null, string AttachmentName = null)
        {
            var _SMTPClient = await connectAsync();

            CCs = CCs ?? new List<string>();
            BCCs = CCs ?? new List<string>();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Recycle Assistant"));

            foreach (var item in TOs)
            {
                message.To.Add(new MailboxAddress(item));
            }

            foreach (var item in CCs)
            {
                message.Cc.Add(new MailboxAddress(item));
            }

            foreach (var item in BCCs)
            {
                message.Bcc.Add(new MailboxAddress(item));
            }


            message.Subject = Subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = Body;
            if (AttachBase64 != null)
            {

                byte[] attachBytes = Convert.FromBase64String(AttachBase64);

                var attachment = new MimePart()
                {
                    Content = new MimeContent(new MemoryStream(attachBytes), ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = AttachmentName
                };
                bodyBuilder.Attachments.Add(attachment);
            }

            message.Body = bodyBuilder.ToMessageBody();

            await _SMTPClient.SendAsync(message);
            await _SMTPClient.DisconnectAsync(true);

            return message;

        }

        public async Task TestSendAsync()
        {
            var _SMTPClient = await connectAsync();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Recycle Assistant"));
            message.To.Add(new MailboxAddress("gomjabar6@gmail.com"));
            message.Subject = "How you doin'?";

            message.Body = new TextPart("plain")
            {
                Text = @"Hey Chandler,

                    I just wanted to let you know that Monica and I were going to go play some paintball, you in?

                    -- Joey"
            };

            await _SMTPClient.SendAsync(message);
            await _SMTPClient.DisconnectAsync(true);

        }

        private async Task<SmtpClient> connectAsync()
        {

            var smtp = new SmtpClient();

            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

            await smtp.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync("helpme@Cyber-Medics.com", @"*Jk-cq3""sKr6T]\h");
            return smtp;

        }
    }
}
