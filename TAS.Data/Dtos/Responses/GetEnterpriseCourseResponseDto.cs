using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Responses
{
    public class GetEnterpriseCourseResponseDto
    {
        public string CourseName { get; set; } = null!;
        public int? Status { get; set; }
        public double CourseCost { get; set; }
        public double Discount { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
