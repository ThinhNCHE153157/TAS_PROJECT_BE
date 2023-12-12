using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Responses
{
    public class GetListPartOfTestResponseDto
    {
        public List<PartOfTestResponseDto> PartAudio { get; set; } = new List<PartOfTestResponseDto>();
        public List<PartOfTestResponseDto> PartReading { get; set; } = new List<PartOfTestResponseDto>();
    }
}
