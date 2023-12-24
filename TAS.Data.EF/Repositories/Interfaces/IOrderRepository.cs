using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities;

namespace TAS.Data.EF.Repositories.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        IQueryable<Order> GetAll();
        IQueryable<Order> GetOrderByAccountId(int accountId);
        bool saveOrder(Order order);
        bool saveVNPayOrder(VnPayHistory vnPayHistory);


    }
}
