using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class Country
    {
        public Country()
        {
            Provinces = new HashSet<Province>();
        }

        public int CountryId { get; set; }
        public string? CountryName { get; set; }

        public virtual ICollection<Province> Provinces { get; set; }
    }
}
