using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Entities
{
    public partial class AccountItemCard
    {
        public int ItemcardId { get; set; }
        public int AccountId { get; set; }
        public int? Status { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual ItemCard Itemcard { get; set; } = null!;
    }
}
