using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Entities
{

    public partial class VnPayHistory
    {
        public int Id { get; set; }
        public int? TransactionId { get; set; }
        public double? Amount { get; set; }
        public int? OrderId { get; set; }

        public virtual Order? Order { get; set; }
    }
}
