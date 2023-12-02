using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class Class
    {
        public Class()
        {
            Accounts = new HashSet<Account>();
            Tests = new HashSet<Test>();
        }

        public int ClassId { get; set; }
        public string? ClassName { get; set; }
        public int MaxStudent { get; set; }
        public string? CreateUser { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Description { get; set; }
        public string? ClassCode { get; set; }
        public string? Subject { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
