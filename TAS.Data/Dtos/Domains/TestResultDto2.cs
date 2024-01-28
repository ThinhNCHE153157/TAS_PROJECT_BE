using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities;

namespace TAS.Data.Dtos.Domains
{
    public class TestResultDto2
    {
        public int TestResultId { get; set; }
        public int AccountId { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public double TestFinish { get; set; }
        public int TestScore { get; set; }
        public int? TestNumberCorrect { get; set; }
        public bool? IsPass { get; set; }
        public int? FeedbackId { get; set; }
        public virtual Test Test { get; set; } = null!;
    }
}
