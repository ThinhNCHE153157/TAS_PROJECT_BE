using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Entities
{
    public partial class ItemCard
    {
        public int Id { get; set; }
        public int? FlashcardId { get; set; }
        public string? NewWord { get; set; }
        public string? Defination { get; set; }
        public string? Image { get; set; }
        public string? Type { get; set; }
        public string? Spelling { get; set; }
        public string? Example { get; set; }
        public string? Note { get; set; }

        public virtual Flashcard? Flashcard { get; set; }
        public virtual ICollection<AccountItemCard> AccountItemCards { get; set; }
    }
}
