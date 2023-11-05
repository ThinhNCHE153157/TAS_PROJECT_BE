using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities;
using TAS.Data.Dtos.Domains;

namespace TAS.Data.Dtos.Responses
{
	public class GetAccountByIdResponseDto
	{
		public int AccountId { get; set; }
		public string? Username { get; set; }
		public string? Password { get; set; }
		public string? Avatar { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public bool? IsVerified { get; set; }
		public string? Otp { get; set; }
		public string? Address { get; set; }
		public string? CreateUser { get; set; }
		public string? UpdateUser { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? UpdateDate { get; set; }
		public bool? IsDeleted { get; set; }

		public virtual ICollection<TestResult> TestResults { get; set; }
		public virtual ICollection<Token> Tokens { get; set; }

		public virtual ICollection<Class> Classes { get; set; }
		public virtual ICollection<Class> ClassesNavigation { get; set; }
		public virtual ICollection<Course> Courses { get; set; }
		public virtual ICollection<Role> Roles { get; set; }
	}
}
