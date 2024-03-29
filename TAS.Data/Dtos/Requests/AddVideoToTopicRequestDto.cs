﻿using Microsoft.AspNetCore.Http;
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
        public string TopicName { get; set; }
        [Required]
        [StringLength(200)]
        public string VideoTitle { get; set; }
        public IFormFile VideoUrl { get; set; } = null!;
        public string? VideoDescription { get; set; }
        public IFormFile? VideoAttachment { get; set; }
    }
}
