using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Responses
{
    public class GetQuestionByCourseIdResponseDto
    {
        public int TestId { get; set; }
        public string Url { get; set; }
        public List<GetQuestionByTestIdResponseDto> Questions { get; set; } = new List<GetQuestionByTestIdResponseDto>();

    }
}
