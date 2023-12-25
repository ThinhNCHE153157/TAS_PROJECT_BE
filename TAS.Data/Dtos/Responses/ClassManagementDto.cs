using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Responses
{
    public class ClassManagementDto
    {
        public int ClassId { get; set; }
        public string? ClassName { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Description { get; set; }
        public string? ClassCode { get; set; }

        public int? MaxStudentInClass { get; set; }
        public string? Subject { get; set; }
        public string? Teacher { get; set; }
    }
}
