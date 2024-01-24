using MimeKit;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;

namespace TAS.Application.Services
{
    public class MailService : IMailService
    {
        private readonly IAccountService _accountService;
        public MailService(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task SendVerifyCode(string email)
        {
            MailRequestDto mailRequest = new MailRequestDto();
            mailRequest.ToEmail = email;
            Random random = new Random();
            int otpNumber = random.Next(1000, 10000);
            string otp = otpNumber.ToString("D4");
            var user = await _accountService.GetUserByEmail(email);
            var result = _accountService.updateOtp(email, otp, System.DateTime.Now.AddMinutes(10));
            mailRequest.Body = $@"
            <html>
            <head>
                <style>
                    body {{
                        font-family: 'Arial', sans-serif;
                        background-color: #f4f4f4;
                        color: #333;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: 0 auto;
                        padding: 20px;
                        background-color: #fff;
                        border-radius: 5px;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    }}
                    h2 {{
                        color: #007BFF;
                    }}
                    .otp {{
                        font-size: 24px;
                        font-weight: bold;
                        color: #28a745;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h2>Email Verification OTP</h2>
                    <p>Your OTP is: <span class='otp'>{otp}</span></p>
                </div>
            </body>
            </html>";
            mailRequest.Subject = "Mã xác thực có hiệu lực trong vòng 10 phút!";
            var x = SendEmailAsync(mailRequest);
        }
        public async Task SendEmailAsync(MailRequestDto mailRequest)
        {
            MailRequestDto mail = new MailRequestDto();
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse("toeicmastersystem@gmail.com");
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("toeicmastersystem@gmail.com", "rpse lwke ibel lbpt");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
