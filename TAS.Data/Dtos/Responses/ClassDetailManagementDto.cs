using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Responses
{
    public class ClassDetailManagementDto
    {
        public int ClassId { get; set; }
        public string? ClassName { get; set; }
        public string? ClassCode { get; set; }
        public List<AccountManageResponseDto> Accounts { get; set; }
    }
}
