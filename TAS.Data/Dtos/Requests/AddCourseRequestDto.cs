using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class AddCourseRequestDto
    {
        public string CourseName { get; set; } 
        public string? CourseDescription { get; set; }
        public int? CourseLevel { get; set; }
    }
}
