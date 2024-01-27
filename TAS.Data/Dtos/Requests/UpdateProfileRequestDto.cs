using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class UpdateProfileRequestDto
    {
        public int AccountId { get; set; }
        public string? Phone { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool? Gender { get; set; }
        //public DateTime? Dob { get; set; }
        public string? Address { get; set; }
    }
}
