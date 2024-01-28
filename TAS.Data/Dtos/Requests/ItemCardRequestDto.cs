using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities;

namespace TAS.Data.Dtos.Requests
{
    public class ItemCardRequestDto
    {
        public int FlashcardId { get; set; }
        public List<ItemCard> ItemCards { get; set; }
    }
}
