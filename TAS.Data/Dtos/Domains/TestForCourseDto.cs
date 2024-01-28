using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities;

namespace TAS.Data.Dtos.Domains
{
    public class TestForCourseDto
    {
        public int TestId { get; set; }
        public string? TestName { get; set; }
        public string ? TestDescription { get; set; }
        public List<PartTestDto> Parts { get; set; }
    }
}
