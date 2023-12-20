using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class AddTopicRequestDto
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string TopicName { get; set; }
        public string? TopicDescription { get; set; }
    }
}
