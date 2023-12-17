using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class AddVideoToTopicRequestDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int TopicId { get; set; }
        [Required]
        [StringLength(50)]
        public string VideoTitle { get; set; }
        public IFormFile VideoPath { get; set; }
    }
}
