﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Responses
{
    public class CourseHomepageResponeDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string? CourseDescription { get; set; }
        public string? Image { get; set; }
        public string? ShortDescription { get; set; }
        public int? CourseLevel { get; set; }
        public double CourseCost { get; set; }
        public double Discount { get; set; }

    }
}
