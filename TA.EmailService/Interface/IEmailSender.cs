using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using TA.EmailService.Models;

namespace TA.EmailService.Interface
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        MimeMessage CreateEmailMessage(Message message);
        void Send(MimeMessage mailMessage);
    }
}
