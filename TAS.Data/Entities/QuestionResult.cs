using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class QuestionResult
    {
        public int TestResultId { get; set; }
        public int QuestionResultId { get; set; }
        public string? Description { get; set; }
        public int? QuestionId { get; set; }
        public int? QuestionAnswerId { get; set; }

        public virtual TestResult? TestResult { get; set; }
    }
}
