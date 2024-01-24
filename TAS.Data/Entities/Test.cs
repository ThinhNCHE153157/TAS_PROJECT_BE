using System;
using System.Collections.Generic;
using TAS.Data.Entities.Interfaces;

namespace TAS.Data.Entities
{
    public partial class Test : IDateTracking
    {
        public Test()
        {
            Parts = new HashSet<Part>();
            TestResults = new HashSet<TestResult>();
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
        public int? TopicId { get; set; }

        public virtual Topic? Topic { get; set; }
        public virtual ICollection<Part> Parts { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}
