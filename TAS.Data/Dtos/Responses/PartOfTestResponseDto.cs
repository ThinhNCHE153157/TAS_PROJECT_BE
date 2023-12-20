using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Responses
{
    public class PartOfTestResponseDto
    {
        public int PartId { get; set; }
        public int NumberOfQuestion { get; set; }

        public PartOfTestResponseDto(int partId, int numberOfQuestion)
        {
            PartId = partId;
            NumberOfQuestion = numberOfQuestion;
        }
    }
}
