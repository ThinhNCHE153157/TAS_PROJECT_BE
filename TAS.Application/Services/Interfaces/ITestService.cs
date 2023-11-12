
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Responses;

﻿using TAS.Data.Dtos.Responses;


namespace TAS.Application.Services.Interfaces
{
    public interface ITestService
    {

        public Task<List<TestHomepageResponeDto>> GetTestHomepage();
        public Task<List<TestDashboardResponseDto>> GetAllTest();
        public Task<GetTestByIdResponseDto> GetTestById(int id);

        public Task<CourseResultResponseDto> CourseResult(int id);

    }
}
