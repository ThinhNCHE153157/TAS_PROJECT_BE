using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Domains;
using TAS.Data.Entities;

namespace TAS.Data.Dtos.Requests
{
    public class UpdateQuestionRequestDto
    {
        public int QuestionId { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string QuestionAnswers { get; set; }
    }
}
