using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities;
using TAS.Data.Dtos.Domains;
using TAS.Data.Dtos.Requests;

namespace TAS.Data.Dtos.Responses
{
	public class GetAccountByIdResponseDto
	{
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? Avatar { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string? Address { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public bool? IsVerified { get; set; }
		public List<RoleRequestDto> Roles { get; set; }

	}
}
