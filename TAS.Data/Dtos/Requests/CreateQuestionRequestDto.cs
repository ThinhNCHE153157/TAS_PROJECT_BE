using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Domains;

namespace TAS.Data.Dtos.Requests
{
    public class CreateQuestionRequestDto
    {
        public int TestId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
        public QuestionAnswerDto QuestionNavigation { get; set; }
    }
}
