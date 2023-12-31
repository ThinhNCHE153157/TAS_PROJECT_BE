﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Responses
{
    public class CourseDashboardResponseDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string? CourseDescription { get; set; }
        public int? CourseLevel { get; set; }
        public string? ShortDescription { get; set; }
        public string? CourseGoal { get; set; }
        public int? Status { get; set; }
        public double CourseCost { get; set; }
        public double Discount { get; set; }
        public string? CreateUser { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
