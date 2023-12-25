using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class QuestionResult
    {
        public QuestionResult()
        {
            QuestionResultAnswers = new HashSet<QuestionResultAnswer>();
        }

        public int? TestResultId { get; set; }
        public int QuestionResultId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Type { get; set; }
        public string? Note { get; set; }

        public virtual TestResult? TestResult { get; set; }
        public virtual ICollection<QuestionResultAnswer> QuestionResultAnswers { get; set; }
    }
}
