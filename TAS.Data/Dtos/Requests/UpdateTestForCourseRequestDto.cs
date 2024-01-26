using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class UpdateTestForCourseRequestDto
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }
        public int PartId { get; set; }
        public int Type { get; set; }
        public IFormFile Url { get; set; }
    }
}
