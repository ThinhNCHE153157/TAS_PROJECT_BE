using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class CreateTestForCourseRequestDto
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public CreateTestRequestDto Tests { get; set; }
    }
}
