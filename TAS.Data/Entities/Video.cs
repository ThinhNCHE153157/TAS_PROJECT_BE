using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities.Interfaces;

namespace TAS.Data.Entities
{
    public partial class Video : IDateTracking
    {
        public int VideoId { get; set; }
        public int? TopicId { get; set; }
        public string VideoUrl { get; set; } = null!;
        public string VideoTitle { get; set; } = null!;
        public string? CreateUser { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Topic? Topic { get; set; }
    }
}
