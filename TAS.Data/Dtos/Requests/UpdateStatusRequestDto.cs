﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class UpdateStatusRequestDto
    {
        public int CourseId { get; set; }
        public int Status { get; set;}
    }
}
