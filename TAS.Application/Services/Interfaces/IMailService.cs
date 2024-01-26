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
        public Task<bool> SendEmailAsync(MailRequestDto mailRequest);
        public Task<bool> SendVerifyCode(string email);
    }
}
