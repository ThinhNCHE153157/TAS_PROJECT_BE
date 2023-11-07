using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Domains
{
    public class TestResultDto
    {
        public int? AccountId { get; set; }
        public int? TestId { get; set; }
        public int TestResultId { get; set; }
        public double? TestFinish { get; set; }
        public int? TestScore { get; set; }
        public int? FeedbackId { get; set; }
    }
}
