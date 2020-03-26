using EVTHJÄLPEN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EVTHJÄLPEN.Services
{
    public class EmailSender
    {
        public void SendEmail(Email em)
        {
            string to = em.to;
            string sub = em.subj;
            string body = em.body;
            MailMessage mm = new MailMessage();
            mm.To.Add(to);
            mm.Subject = sub;
            mm.Body = body;
            mm.From = new MailAddress("evthjalpen@gmail.com");
            mm.IsBodyHtml = true;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
            };

            //TODO: Inte ha ett hårdkodat lösenord. Fixa sen. 
            smtp.Credentials = new System.Net.NetworkCredential("evthjalpen@gmail.com", "Event1337");
            smtp.Send(mm);
        }
    }
}
