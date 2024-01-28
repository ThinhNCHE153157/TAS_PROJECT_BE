using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class AddFlashCardItemRequestDto
    {
        public int? FlashcardId { get; set; }
        public string? NewWord { get; set; }
        public string? Defination { get; set; }
        public string? Example { get; set; }
        public string? Note { get; set; }
        public IFormFile? Image { get; set; }
        public string? Type { get; set; }
        public string? Spelling { get; set; }
    }
}
