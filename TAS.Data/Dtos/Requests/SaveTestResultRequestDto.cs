using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Domains;

namespace TAS.Data.Dtos.Requests
{
    public class SaveTestResultRequestDto
    {
        public int AccountId { get; set; }
        public int TestId { get; set; }
        public double TestFinish { get; set; }
        public double TestScore { get; set; }
        public string NumberCorrect { get; set; }
        public List<UserAnswerDto> ListAnswer { get; set; }

    }
}
