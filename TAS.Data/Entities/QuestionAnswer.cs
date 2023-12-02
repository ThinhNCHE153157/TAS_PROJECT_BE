using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class QuestionAnswer
    {
        public int QuestionAnswerId { get; set; }
        public int? QuestionId { get; set; }
        public string? ResultA { get; set; }
        public string? ResultB { get; set; }
        public string? ResultC { get; set; }
        public string? ResultD { get; set; }
        public string? CorrectResult { get; set; }

        public virtual Question? Question { get; set; }
    }
}
