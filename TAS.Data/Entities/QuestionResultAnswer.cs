using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class QuestionResultAnswer
    {
        public int QuestionResultAnswerId { get; set; }
        public int? QuestionResultId { get; set; }
        public string? ResultA { get; set; }
        public string? ResultB { get; set; }
        public string? ResultC { get; set; }
        public string? ResultD { get; set; }
        public string? CorrectResult { get; set; }
        public string? Seleted { get; set; }

        public virtual QuestionResult? QuestionResult { get; set; }
    }
}
