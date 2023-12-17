using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class AddTopicRequestDto
    {
        public int CourseId { get; set; }
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
    }
}
