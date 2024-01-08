using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class QuestionAnswer
    {
        public int QuestionAnswerId { get; set; }
        public int? QuestionId { get; set; }
        public string? Answer { get; set; }
        public string? Explanation { get; set; }
        public bool? Iscorrect { get; set; }

        public virtual Question? Question { get; set; }
    }
}
