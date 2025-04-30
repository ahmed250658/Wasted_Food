using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using Wasted_Food.Data.Entities.Identity;
using Wasted_Food.Data.Helper;
using Wasted_Food.Infrastructure.Data;
using Wasted_Food.Service.Abstracts;

namespace Wasted_Food.Service.Implementions
{
    public class EmailService : IEmailService
    {
        #region Fields
        private readonly UserManager<Users> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly EmailSettings _emailSettings;
        #endregion

        #region Constructor
        public EmailService(AppDbContext dbContext, UserManager<Users> userManager, EmailSettings emailSettings)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _emailSettings = emailSettings;
        }

        #endregion

        #region Handler Function
        public async Task<string> SendEmail(string email, string Message, string? reason)
        {
            try
            {
                //sending the Message of passwordResetLink
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                    client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{Message}",
                        TextBody = "wellcome",
                    };
                    var message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("Future Team", _emailSettings.FromEmail));
                    message.To.Add(new MailboxAddress("testing", email));
                    message.Subject = reason == null ? "Not Sumitted" : reason;
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                //end of sending email
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed";
            }
        }
        #endregion
    }
}
