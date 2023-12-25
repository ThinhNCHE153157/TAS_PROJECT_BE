using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Requests;

namespace TAS.Application.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequestDto mailRequest);
        Task SendVerifyCode(string email);
    }
}
