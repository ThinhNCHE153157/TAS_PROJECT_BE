using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class Course
    {
        public Course()
        {
            Topics = new HashSet<Topic>();
            Accounts = new HashSet<Account>();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public string? CourseDescription { get; set; }
        public int? CourseLevel { get; set; }
        public double CourseCost { get; set; }
        public double Discount { get; set; }
        public string? CreateUser { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
