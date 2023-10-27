﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF;
using TAS.Data.Entities;

namespace TAS.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CourseService (IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CourseDashboardResponseDto>> GetAllCourse()
        {
            try
            {
                var listCourse = await _unitOfWork.CourseRepository.GetAllCourses().ToListAsync().ConfigureAwait(false);
                var result = _mapper.Map<List<CourseDashboardResponseDto>>(listCourse);
                return result;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<List<CourseHomepageResponeDto>> getCourseHomepage()
        {
            try
            {
            var listCourse = await _unitOfWork.CourseRepository.GetAllCourses().ToListAsync().ConfigureAwait(false);
            var result = _mapper.Map<List<CourseHomepageResponeDto>>(listCourse);
            return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
