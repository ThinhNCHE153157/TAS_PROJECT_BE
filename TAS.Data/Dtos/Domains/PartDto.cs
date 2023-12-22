using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities;

namespace TAS.Data.Dtos.Domains
{
    public class PartDto
    {
        public int PartId { get; set; }
        public virtual ICollection<QuestionDto> Questions { get; set; }
    }
}
