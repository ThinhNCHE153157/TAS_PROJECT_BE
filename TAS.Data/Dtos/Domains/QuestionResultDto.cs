using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Domains
{
    public class QuestionResultDto
    {
        public int TestResultId { get; set; }
        public string? Description { get; set; }
        public int? QuestionId { get; set; }
    }
}
