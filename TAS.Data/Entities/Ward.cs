using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class Ward
    {
        public Ward()
        {
            Addresses = new HashSet<Address>();
        }

        public int? DistrictsId { get; set; }
        public int WardsId { get; set; }
        public string? WardsName { get; set; }

        public virtual District? Districts { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
