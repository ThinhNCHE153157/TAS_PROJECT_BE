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
using TAS.Data.S3Object;

namespace TAS.Application.Services
{
    public class FlashcardService : IFlashcardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<FlashcardService> _logger;
        private readonly IS3StorageService _s3StorageService;

        public FlashcardService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<FlashcardService> logger, IS3StorageService s3StorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _s3StorageService = s3StorageService;
        }

        public async Task<bool> AddFlashCardItem(AddFlashCardItemRequestDto request)
        {
            try
            {
                if (request != null)
                {
                    var itemcard = _mapper.Map<ItemCard>(request);
                    if (request.Image != null)
                    {
                        S3RequestData s3RequestData = new S3RequestData
                        {
                            BucketName = "tas",
                            InputStream = request.Image.OpenReadStream(),
                            Name = request.Image.FileName,
                        };
                        await _s3StorageService.UploadFileAsync(s3RequestData).ConfigureAwait(false);
                        itemcard.Image = _s3StorageService.GetFileUrlDontExpires(s3RequestData);
                    }
                    var result = _unitOfWork.FlashcardRepository.AddFlashCardItem(itemcard);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> AddItemcard(List<AddItemCardRequestDto> request)
        {
            try
            {
                var itemcard = _mapper.Map<List<ItemCard>>(request);
                var result = _unitOfWork.FlashcardRepository.AddItemcard(itemcard);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
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
                return false;
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

        public async Task<bool> DeleteItemcard(int id)
        {
            try
            {
                var result = _unitOfWork.FlashcardRepository.DeleteItemcard(id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<GetFlashcardByAccountIdResponseDto>> GetFlashCardByAccountId(int accountId)
        {
            try
            {
                var result = _unitOfWork.FlashcardRepository.GetFlashCardByAccountId(accountId);
                return result;
                //var listaccountFlashcard = _unitOfWork.FlashcardRepository.GetAccountFlashcardsByAccountId(accountId);
                //var response = _mapper.Map<List<GetFlashcardByAccountIdResponseDto>>(result);
                //return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<GetFlashcardByFlashcardResponseDto> GetFlashCardByFlashcardId(int flashcardid, int accountid)
        {
            try
            {
                var result = _unitOfWork.FlashcardRepository.GetFlashCardByFlashcardId(flashcardid);
                var account = _unitOfWork.FlashcardRepository.GetAccountOwner(flashcardid);
                var response = _mapper.Map<GetFlashcardByFlashcardResponseDto>(result);
                response.AccountId = account.AccountId;
                if (account.AccountId == accountid)
                {
                    response.IsOwn = true;
                }
                else
                {
                    response.IsOwn = false;
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
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

        public async Task<bool> UpdateItemcard(UpdateItemRequestDto request)
        {
            try
            {
                var itemcard = _unitOfWork.FlashcardRepository.GetItemCardById(request.Id);
                if (itemcard != null)
                {
                    itemcard.NewWord = request.NewWord;
                    itemcard.Defination = request.Defination;
                    itemcard.Example = request.Example;
                    itemcard.Note = request.Note;
                    itemcard.Type = request.Type;
                    itemcard.Spelling = request.Spelling;
                    if (request.Image != null)
                    {
                        S3RequestData s3RequestData = new S3RequestData
                        {
                            BucketName = "tas",
                            InputStream = request.Image.OpenReadStream(),
                            Name = request.Image.FileName,
                        };
                        await _s3StorageService.UploadFileAsync(s3RequestData).ConfigureAwait(false);
                        itemcard.Image = _s3StorageService.GetFileUrlDontExpires(s3RequestData);
                    }
                    _unitOfWork.Commit();
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
