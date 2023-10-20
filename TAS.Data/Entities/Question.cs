using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class Question
    {
        public int? TestId { get; set; }
        public int QuestionId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Type { get; set; }
        public string? Note { get; set; }
        public string? CreateUser { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual QuestionAnswer QuestionNavigation { get; set; } = null!;
        public virtual Test? Test { get; set; }
    }
}
