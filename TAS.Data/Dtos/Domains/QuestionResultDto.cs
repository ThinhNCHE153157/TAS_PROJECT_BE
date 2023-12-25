using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Domains
{
    public class QuestionResultDto
    {
        public int? TestResultId { get; set; }
        public string ? Description { get; set; }
        public string? Image { get; set; }
        public string? Type { get; set; }

        public QuestionResultDto(int? testResultId,string description, string? image, string? type)
        {
            TestResultId = testResultId;
            Description = description;
            Image = image;
            Type = type;
        }
    }
}
