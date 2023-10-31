using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class Address
    {
        public int? WardsId { get; set; }
        public int AddressId { get; set; }
        public string? AddressName { get; set; }

        public virtual Ward? Wards { get; set; }
    }
}
