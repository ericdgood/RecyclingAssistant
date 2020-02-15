using MailKit.Net.Smtp;
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
        public MimeMessage Send(List<string> TOs, string Subject, string Body, List<string> CCs = null, List<string> BCCs = null, string AttachBase64 = null, string AttachmentName = null)
        {
            var _SMTPClient = connect();

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

            _SMTPClient.Send(message);
            _SMTPClient.Disconnect(true);

            return message;

        }

        public void TestSend()
        {
            var _SMTPClient = connect();

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

            _SMTPClient.Send(message);
            _SMTPClient.Disconnect(true);

        }

        private SmtpClient connect()
        {

            var _SMTPClient = new SmtpClient();

            _SMTPClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

            var clientAddress = "smtp.office365.com";
            var Port = "587";

            _SMTPClient.Connect(
                clientAddress,
                Convert.ToInt32(Convert.ToDouble(Port)),
                MailKit.Security.SecureSocketOptions.StartTls
                );

            return _SMTPClient;

        }
    }
}
