using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Entities
{
    public partial class Part
    {
        public Part()
        {
            Questions = new HashSet<Question>();
        }

        public int PartId { get; set; }
        public int? TestId { get; set; }
        public bool? Type { get; set; }

        public virtual Test? Test { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
