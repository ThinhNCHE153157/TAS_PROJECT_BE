using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Entities
{
    public partial class Order
    {
        public Order()
        {
            VnPayHistories = new HashSet<VnPayHistory>();
        }

        public string OrderId { get; set; } = null!;
        public int? AccountId { get; set; }
        public int? CourseId { get; set; }
        public double? TotalAmount { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ICollection<VnPayHistory> VnPayHistories { get; set; }
    }
}
