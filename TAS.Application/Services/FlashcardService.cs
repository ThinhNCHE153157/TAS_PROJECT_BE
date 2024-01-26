using Amazon.Runtime.Internal.Util;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF;
using TAS.Data.EF.Repositories.Interfaces;
using TAS.Data.Entities;

namespace TAS.Application.Services
{
    public class FlashcardService : IFlashcardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<FlashcardService> _logger;

        public FlashcardService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<FlashcardService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CreateFlashCard(FlashCardRequestDto request)
        {
            try
            {
                var flashcard = _mapper.Map<Flashcard>(request);
                var result = _unitOfWork.FlashcardRepository.CreateFlashCard(flashcard);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteFlashCard(int id)
        {
            try
            {
                var result = _unitOfWork.FlashcardRepository.DeleteFlashCard(id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<GetFlashcardByAccountIdResponseDto>> GetFlashCardByAccountId(int accountId)
        {
            try
            {
                var result = _unitOfWork.FlashcardRepository.GetFlashCardByAccountId(accountId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateFlashCard(FlashCardRequestDto request, int id)
        {
            try
            {
                var result = _unitOfWork.FlashcardRepository.UpdateFlashCard(request, id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
