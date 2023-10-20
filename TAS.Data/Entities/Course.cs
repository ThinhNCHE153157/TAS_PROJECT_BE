using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class Course
    {
        public Course()
        {
            Accounts = new HashSet<Account>();
            Tests = new HashSet<Test>();
        }

        public int CourseId { get; set; }
        public string? CourseDescription { get; set; }
        public int? CourseLevel { get; set; }
        public string? CreateUser { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
