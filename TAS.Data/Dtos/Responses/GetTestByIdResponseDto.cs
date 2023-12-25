using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Domains;
using TAS.Data.Entities;

namespace TAS.Data.Dtos.Responses
{
    public class GetTestByIdResponseDto
    {
        public int TestId { get; set; }
        public string? TestName { get; set; }
        public double? TestDuration { get; set; }
        public double? TestTotalScore { get; set; }
        public string? TestDescription { get; set; }
        public virtual ICollection<PartDto> Parts { get; set; }
    }
}
