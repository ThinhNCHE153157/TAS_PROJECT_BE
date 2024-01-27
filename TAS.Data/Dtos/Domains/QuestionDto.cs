using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities;

namespace TAS.Data.Dtos.Domains
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<QuestionAnswerDto> questionAnswers { get; set; }
    }
}
