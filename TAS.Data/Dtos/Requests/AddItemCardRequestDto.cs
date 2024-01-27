using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Requests
{
    public class AddItemCardRequestDto
    {
        public int? FlashcardId { get; set; }
        public string? NewWord { get; set; }
        public string? Defination { get; set; }
        public string? Example { get; set; }
        public string? Note { get; set; }
    }
}
