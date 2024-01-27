using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Requests;

namespace TAS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FlashCardController : ControllerBase
    {
        private readonly IFlashcardService _flashcardService;
        public FlashCardController(IFlashcardService flashCardService)
        {
            _flashcardService = flashCardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlashCardByAccountId([FromQuery] int id)
        {
            var data = await _flashcardService.GetFlashCardByAccountId(id);
            return Ok(data);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateFlashCard([FromBody] FlashCardRequestDto request)
        {
            var data = await _flashcardService.CreateFlashCard(request);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFlashCard([FromBody] FlashCardRequestDto request, [FromQuery] int id)
        {
            var data = await _flashcardService.UpdateFlashCard(request,id);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFlashCard([FromQuery] int id)
        {
            var data = await _flashcardService.DeleteFlashCard(id);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItemCard([FromBody] List<AddItemCardRequestDto> request)
        {
            var data = await _flashcardService.AddItemcard(request);
            return Ok(data);
        }
    }
}
