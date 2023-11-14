using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Domains
{
    public class QuestionAnswerDto
    {
        public int QuestionId { get; set; }
        public string? ResultA { get; set; }
        public string? ResultB { get; set; }
        public string? ResultC { get; set; }
        public string? ResultD { get; set; }
        public string? CorrectResult { get; set; }
    }
}
