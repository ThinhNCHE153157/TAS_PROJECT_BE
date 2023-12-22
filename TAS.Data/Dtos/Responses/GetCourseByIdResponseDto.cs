using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Domains;
using TAS.Data.Entities;

namespace TAS.Data.Dtos.Responses
{
    public class GetCourseByIdResponseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string? Image { get; set; }
        public string? ShortDescription { get; set; }
        public string? CourseGoal { get; set; }
        public string? CourseDescription { get; set; }
        public int? CourseLevel { get; set; }
        public double CourseCost { get; set; }
        public double Discount { get; set; }
        public virtual ICollection<TopicDto> Topics { get; set; }
    }
}
