using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Responses
{
    public class GetListTestFreeResponseDto
    {
        public int TestId { get; set; }
        public string? TestName { get; set; }
        public double? TestDuration { get; set; }
        public double? TestTotalScore { get; set; }
        public string? TestDescription { get; set; }
        public int? TotalPart { get; set; }
        public int ? TestTotalQuestion { get; set; }
    }
}
