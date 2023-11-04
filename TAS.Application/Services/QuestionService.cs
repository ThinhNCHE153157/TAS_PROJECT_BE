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

namespace TAS.Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<QuestionDashboardResponseDto>> GetAllQuestion()
        {
            try
            {
                var listQuestion = await _unitOfWork.QuestionRepository.GetAllQuestion().ToListAsync().ConfigureAwait(false);
                var result = _mapper.Map<List<QuestionDashboardResponseDto>>(listQuestion);
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<GetQuestionByIdResponseDto> GetQuestionById(int id)
        {
            try
            {
                var question = await _unitOfWork.QuestionRepository.GetQuestionById(id).Include(x => x.Test).FirstOrDefaultAsync().ConfigureAwait(false);
                var result = _mapper.Map<GetQuestionByIdResponseDto>(question);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return null;
        }


        public async Task<List<QuestionHomepageResponeDto>> getQuestionHomepage()
        {
            try
            {
                var listQuestion = await _unitOfWork.QuestionRepository.GetAllQuestion().ToListAsync().ConfigureAwait(false);
                var result = _mapper.Map<List<QuestionHomepageResponeDto>>(listQuestion);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}