using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class TestResult
    {
        public TestResult()
        {
            QuestionResults = new HashSet<QuestionResult>();
        }

        public int TestResultId { get; set; }
        public int AccountId { get; set; }
        public int TestId { get; set; }
        public double TestFinish { get; set; }
        public int TestScore { get; set; }
        public int? TestNumberCorrect { get; set; }
        public bool? IsPass { get; set; }
        public int? FeedbackId { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Test Test { get; set; } = null!;
        public virtual ICollection<QuestionResult> QuestionResults { get; set; }
    }
}
