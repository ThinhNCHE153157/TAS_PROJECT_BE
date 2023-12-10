﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class GetQuestionByTestIdRequestDto
    {
        public int TestId { get; set; }

        public GetQuestionByTestIdRequestDto()
        {
        }

        public GetQuestionByTestIdRequestDto(int testId)
        {
            TestId = testId;
        }
    }
}
