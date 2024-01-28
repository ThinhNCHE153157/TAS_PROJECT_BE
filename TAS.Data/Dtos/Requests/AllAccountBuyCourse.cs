using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class AllAccountBuyCourse
    {
        public string UserName { get; set; } 
        public string Email { get; set; }
        public string CourseName { get; set; }
        public double CourseCost { get; set; }
        public double Discount { get; set; }

    }
}
