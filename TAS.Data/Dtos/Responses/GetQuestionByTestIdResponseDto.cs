using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Responses
{
    public class GetQuestionByTestIdResponseDto
    {
        public int QuestionId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? ResultA { get; set; }
        public string? ResultB { get; set; }
        public string? ResultC { get; set; }
        public string? ResultD { get; set; }
        public string? CorrectResult { get; set; }
    }
}
