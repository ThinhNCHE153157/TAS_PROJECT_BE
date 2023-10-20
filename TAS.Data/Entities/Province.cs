using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class Province
    {
        public Province()
        {
            Cities = new HashSet<City>();
        }

        public int? CountryId { get; set; }
        public int ProvincesId { get; set; }
        public string? ProvincesName { get; set; }

        public virtual Country? Country { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}
