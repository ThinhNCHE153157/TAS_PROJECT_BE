using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Dtos.Domains;

namespace TAS.Data.Dtos.Responses
{
    public class GetFlashcardByFlashcardResponseDto
    {
        public int FlashcardId { get; set; }
        public int AccountId { get; set; }
        public string FlashcardName { get; set; }
        public string Description { get; set; }
        public bool? IsOwn { get; set; }
        public string CreateUser { get; set; }
        public int NumberOfItem { get; set; }
        public List<ItemCardDto> ItemCards { get; set; }
    }
}
