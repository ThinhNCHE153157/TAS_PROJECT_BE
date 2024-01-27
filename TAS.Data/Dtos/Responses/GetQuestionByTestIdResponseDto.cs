using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Domains;

namespace TAS.Data.Dtos.Responses
{
    public class GetQuestionByTestIdResponseDto
    {
        public int QuestionId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<GetQuestionAnswerDto> QuestionAnswers { get; set; } = new List<GetQuestionAnswerDto>();
    }
}
