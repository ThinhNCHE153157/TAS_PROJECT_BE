using Amazon.Runtime.Internal.Util;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Application.Services.Interfaces;
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
        private readonly ILogger<FlashcardService>  _logger;

        public FlashcardService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<FlashcardService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
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
    }
}
