using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class AddVideoOfTopicRequestDto
    {
        public IFormFile VideoUrl { get; set; } = null!;
        public string VideoTitle { get; set; } = null!;
        public string? VideoDescription { get; set; }
        public IFormFile? VideoAttachment { get; set; }
    }
}
