using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Entities
{
    public partial class AccountFlashcard
    {
        public int FlashcardId { get; set; }
        public int AccountId { get; set; }
        public bool? IsOwn { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual Flashcard Flashcard { get; set; } = null!;
    }
}
