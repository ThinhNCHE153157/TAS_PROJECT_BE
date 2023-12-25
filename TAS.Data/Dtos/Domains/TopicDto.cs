using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Domains
{
    public class TopicDto
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; } = null!;
        public string? TopicDescription { get; set; }
        public int? CourseId { get; set; }
    }
}
