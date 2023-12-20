using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class UpdateAvatarRequestDto
    {
        [Required]
        public int AccountId { get; set; }
        [Required]
        public IFormFile Avatar { get; set; }
    }
}
