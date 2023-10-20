using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class City
    {
        public City()
        {
            Districts = new HashSet<District>();
        }

        public int? ProvincesId { get; set; }
        public int CitiesId { get; set; }
        public string? CitiesName { get; set; }

        public virtual Province? Provinces { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}
