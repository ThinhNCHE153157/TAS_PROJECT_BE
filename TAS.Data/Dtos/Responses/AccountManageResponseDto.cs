using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities;

namespace TAS.Data.Dtos.Responses
{
    public class AccountManageResponseDto
    {
        public int AccountId { get; set; }
        public string? Username { get; set; }
        public string? Avatar { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string ? Address { get; set; }
        public DateTime? CreateDate { get; set; }
        public List<string> ?RoleNames { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
