using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Domains;
using TAS.Data.Entities;

namespace TAS.Data.Dtos.Responses
{
    public class GetTopicByCourseIdResponseDto
    {
        public string CourseName { get; set; }
        public int  TopicId { get; set; }
        public string  TopicName { get; set; }
        public virtual ICollection<VideoDto> Videos { get; set; }
        public int? PartId { get; set; }
    }
}
