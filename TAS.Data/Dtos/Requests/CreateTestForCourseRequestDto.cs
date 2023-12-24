using Microsoft.AspNetCore.Http;
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
        public int TopicId { get; set; }
        public string TestName { get; set; }
        public int Type { get; set; } 
        public IFormFile? Url { get; set; }
    }
}
