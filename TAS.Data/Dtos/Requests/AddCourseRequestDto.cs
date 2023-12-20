using Microsoft.AspNetCore.Http;

namespace TAS.Data.Dtos.Requests
{
    public class AddCourseRequestDto
    {
        public string CourseName { get; set; }
        public string? CourseDescription { get; set; }
        public IFormFile? Image { get; set; }
        public string? ShortDescription { get; set; }
        public string? CourseGoal { get; set; }
        public int? CourseLevel { get; set; }
    }
}
