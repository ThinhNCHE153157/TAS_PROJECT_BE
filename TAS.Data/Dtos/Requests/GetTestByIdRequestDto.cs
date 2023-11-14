using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class GetTestByIdRequestDto
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int TestId { get; set; }
    }
}
