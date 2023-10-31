using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities;

namespace TAS.Data.Dtos.Responses
{
	public partial class AccountHomepageResponeDTO
	{
		public int AccountId { get; set; }
		public string? Username { get; set; }
		public string? Avatar { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public DateTime? CreateDate { get; set; }
		public virtual ICollection<Role> Roles { get; set; }
		public bool? IsDeleted { get; set; }
	}
}
