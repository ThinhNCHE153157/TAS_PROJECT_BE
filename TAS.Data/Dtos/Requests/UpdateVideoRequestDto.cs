using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class UpdateVideoRequestDto
    {
        public int VideoId { get; set; }
        public IFormFile Video { get; set; }
    }
}
