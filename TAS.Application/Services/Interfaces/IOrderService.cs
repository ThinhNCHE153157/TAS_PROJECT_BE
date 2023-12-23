using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Responses;
using TAS.Data.Entities;

namespace TAS.Application.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<Order> SaveOrder(PaymentResponseModel response);
    }
}
