using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class UpdateStatusTestDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int TestId { get; set; }
    }
}
