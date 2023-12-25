using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class UpdateCostRequestDto
    {
        public int CourseId { get; set; }
        public double Price { get; set;}
    }
}
