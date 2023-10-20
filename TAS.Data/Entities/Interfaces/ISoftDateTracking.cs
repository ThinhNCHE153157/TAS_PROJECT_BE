using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Entities.Interfaces
{
    public interface ISoftDateTracking
    {
        public bool IsDelete { get; set; }
    }
}
