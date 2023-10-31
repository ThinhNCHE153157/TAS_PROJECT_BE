using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class District
    {
        public District()
        {
            Wards = new HashSet<Ward>();
        }

        public int? CitiesId { get; set; }
        public int DistrictsId { get; set; }
        public string? DistrictsName { get; set; }

        public virtual City? Cities { get; set; }
        public virtual ICollection<Ward> Wards { get; set; }
    }
}
