using dirtbike.api.Data;
using dirtbike.api.Models;
using System.Diagnostics;
using System.Text;
using Azure.Messaging.ServiceBus;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Enterpriseservices
{

  public class EmailNotifiers
  {


public void linuxsendnotification(int uid, string emailAddress, string message)
{
    if (string.IsNullOrWhiteSpace(emailAddress))
        return;

    // 1. Write the notification to the database
    using (var db = new DirtbikeContext())
    {
        var notice = new Usernotice
        {
            Userid = uid,
            Useridstring = uid.ToString(),
            Description = message,
            Noticetype = "email",
            Emailgwtype = "sendmail",
            NoticeDatetime = DateTime.UtcNow
        };

        db.Usernotices.Add(notice);
        db.SaveChanges();
    }

    // 2. Build the email content for sendmail
    var sb = new StringBuilder();
    sb.AppendLine($"To: {emailAddress}");
    sb.AppendLine("Subject: Notification");
    sb.AppendLine("From: no-reply@yourdomain.com");
    sb.AppendLine();
    sb.AppendLine(message);

    // 3. Execute sendmail
    var process = new Process();
    process.StartInfo.FileName = "/usr/sbin/sendmail";   // adjust if needed
    process.StartInfo.Arguments = "-t";
    process.StartInfo.RedirectStandardInput = true;
    process.StartInfo.UseShellExecute = false;

    process.Start();
    process.StandardInput.Write(sb.ToString());
    process.StandardInput.Close();
    process.WaitForExit();
}



public async Task azuresendnotificationasync(int uid, string emailAddress, string message)
{
    // 1. Log the notification to the database
    using (var db = new DirtbikeContext())
    {
        var notice = new Usernotice
        {
            Userid = uid,
            Useridstring = uid.ToString(),
            Description = message,
            Noticetype = "email",
            Emailgwtype = "servicebus",
            NoticeDatetime = DateTime.UtcNow,
           };

        db.Usernotices.Add(notice);
        db.SaveChanges();
    }

    // 2. Build the payload for the email microservice
    var payload = new
    {
        uid = uid,
        email = emailAddress,
        message = message,
        timestamp = DateTime.UtcNow
    };

    string json = JsonSerializer.Serialize(payload);

    // 3. Send to Azure Service Bus
    string connectionString = "<YOUR SERVICE BUS CONNECTION STRING>";
    string queueName = "<YOUR QUEUE NAME>";

    await using var client = new ServiceBusClient(connectionString);
    ServiceBusSender sender = client.CreateSender(queueName);

    var sbMessage = new ServiceBusMessage(json)
    {
        ContentType = "application/json",
        Subject = "email-notification"
    };

    await sender.SendMessageAsync(sbMessage);
}

public async Task gmailsendnotificationasync(int uid, string emailAddress, string message)
{
    // 1. Log the notification to the database
    using (var db = new DirtbikeContext())
    {
        var notice = new Usernotice
        {
            Userid = uid,
            Useridstring = uid.ToString(),
            Description = message,
            Noticetype = "email",
            Emailgwtype = "gmail",
            NoticeDatetime = DateTime.UtcNow,
            Emailaddress = emailAddress
        };

        db.Usernotices.Add(notice);
        db.SaveChanges();
    }

    // 2. Build the email
    var mail = new MailMessage();
    mail.From = new MailAddress("547bikes.info@gmail.com");
    mail.To.Add(emailAddress);
    mail.Subject = "Notification";
    mail.Body = message;

    // 3. Configure Gmail SMTP
    var smtp = new SmtpClient("smtp.gmail.com", 587)
    {
        EnableSsl = true,
        Credentials = new NetworkCredential(
            "547bikes.info@gmail.com",
            "*Columbia5"   // <-- replace with secure storage
        )
    };

    // 4. Send email
    await smtp.SendMailAsync(mail);
}

public async Task AddGuestNotice(WebApplication app, string to, string body, string status)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<DirtbikeContext>();

    var notice = new Usernotice
    {
        Userid = 901,
        Useridstring = "901",
        Description = body,
        Noticetype = "email",
        Emailgwtype = status,
        NoticeDatetime = DateTime.UtcNow,
        Emailaddress = to,
    };

    db.Usernotices.Add(notice);
    await db.SaveChangesAsync();
}

}}
