using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class AddEnterpriseRequestDto
    {
        public int AccountId { get; set; }
        public string EnterpriseCode { get; set; } = null!;
        public string EnterpriseName { get; set; } = null!;
        public string? ForeignName { get; set; }
        public string ShortName { get; set; } = null!;
        public string RepresentativeName { get; set; } = null!;
        public string OfficeAddress { get; set; } = null!;
        //public int Status { get; set; }
    }
}
