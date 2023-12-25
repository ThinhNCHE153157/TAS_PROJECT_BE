using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Domains
{
    public class UserAnswerDto
    {
        public int? QuestionId { get; set; }
        public string? UserAnswer { get; set; }
    }
}
