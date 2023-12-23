using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Domains;

namespace TAS.Data.Dtos.Responses
{
    public class SaveTestResultResponseDto
    {
        public string TestName { get; set; }    
        public string? NumCorrect { get; set; }
        public List<UserAnswerDto> userAnswers { get; set; } = new List<UserAnswerDto>();
        public List<QuestionDto> questionDtos { get; set; } = new List<QuestionDto>();
    }
}
