using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;
using System.Net;
using System.Net.Mail;

namespace Domain.Entities
{
    public class EmailSettings
    {
        public string MailToAddress = "someemail@gmail.com";
        public string MailFromAddress = "youremail@gmail.ru";
        public bool UseSsl = true;
        public string Username = "Name";
        public string Password = "Pass";
        public string ServerHost = "smtp.gmail.com";
        public int ServerPort = 25; //или 587
        public bool WriteAsFile = true;
        public string FileLocation = @"C:\Users\Dom\Documents\Visual Studio 2015\Projects\GameStation\";
    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(GameCart cart, ShippingInfo info)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.Host = emailSettings.ServerHost;
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("Новый заказ обработан")
                    .AppendLine("---")
                    .AppendLine("Товары:");

                foreach (var line in cart.lines)
                {
                    var subtotal = line.Game.Price * line.Quanity;
                    body.AppendFormat("{0} x {1} (итого: {2:c}",
                        line.Quanity, line.Game.Title, subtotal);   
                }

                body.AppendFormat("Общая стоимость: {0:c}", cart.CalculateTotalPrice())
                    .AppendLine("---")
                    .AppendLine("Доставка:")
                    .AppendLine(info.Name)
                    .AppendLine(info.Address1)
                    .AppendLine(info.Address2 ?? "")
                    .AppendLine(info.Address3 ?? "")
                    .AppendLine(info.City)
                    .AppendLine(info.Country)
                    .AppendLine("---")
                    .AppendFormat("Подарочная упаковка: {0}",
                        info.GiftWrap ? "Да" : "Нет");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	// От кого
                                       emailSettings.MailToAddress,		// Кому
                                       "Новый заказ отправлен!",		// Тема
                                       body.ToString()); 				// Тело письма

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);

            }
        }
    }
}
