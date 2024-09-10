using System;
using System.Net;
using System.Net.Mail;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            SmtpClient smtpClient = new SmtpClient("test")
            {
                Port = 5870, 
                Credentials = new NetworkCredential("sadkjfhlfe@gmail.com", "sadkjfhlfe"),
                EnableSsl = true 
            };

            MailMessage mail = new MailMessage
            {
                From = new MailAddress("sadkjfhlfe@gmail.com"),
                Subject = "test",
                Body = "test",
            };

            mail.To.Add("test@gmail.com");
            mail.To.Add("test@gmail.com");

            smtpClient.Send(mail);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
