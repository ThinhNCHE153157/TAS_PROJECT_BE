using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class UpdateTestRequestDto
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int TestId { get; set; }
        public string? TestName { get; set; }
        public double? TestDuration { get; set; }
        public double? TestTotalScore { get; set; }
        public string? TestDescription { get; set; }
    }
}
