using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Entities
{
    public partial class Topic
    {
        public Topic()
        {
            Tests = new HashSet<Test>();
            Videos = new HashSet<Video>();
        }

        public int TopicId { get; set; }
        public string TopicName { get; set; } = null!;
        public string? TopicDescription { get; set; }
        public int? CourseId { get; set; }
        public string? CreateUser { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Course? Course { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}
