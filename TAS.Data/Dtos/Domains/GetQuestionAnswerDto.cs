using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Domains
{
    public class GetQuestionAnswerDto
    {
        public int QuestionAnswerId { get; set; }
        public string? Answer { get; set; }
        public bool? Iscorrect { get; set; }
    }
}
