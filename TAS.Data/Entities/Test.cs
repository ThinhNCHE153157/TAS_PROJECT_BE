using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class Test
    {
        public Test()
        {
            Questions = new HashSet<Question>();
            TestResults = new HashSet<TestResult>();
            Classes = new HashSet<Class>();
            Courses = new HashSet<Course>();
        }

        public int TestId { get; set; }
        public string? TestName { get; set; }
        public double? TestDuration { get; set; }
        public double? TestTotalScore { get; set; }
        public string? TestDescription { get; set; }
        public string? CreateUser { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
