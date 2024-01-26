using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Data.Entities.Interfaces;

namespace TAS.Data.Entities
{
    public partial class Flashcard : IDateTracking
    {
        public Flashcard()
        {
            AccountFlashcards = new HashSet<AccountFlashcard>();
            ItemCards = new HashSet<ItemCard>();
        }

        public int FlashcardId { get; set; }
        public string? Description { get; set; }
        public string? CreateUser { get; set; }
        public string? UpdateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<AccountFlashcard> AccountFlashcards { get; set; }
        public virtual ICollection<ItemCard> ItemCards { get; set; }
    }
}
