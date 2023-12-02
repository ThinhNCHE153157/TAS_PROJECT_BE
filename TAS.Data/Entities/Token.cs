using System;
using System.Collections.Generic;

namespace TAS.Data.Entities
{
    public partial class Token
    {
        public int? AccountId { get; set; }
        public int TokenId { get; set; }
        public string? Token1 { get; set; }
        public string? TokenType { get; set; }
        public int? Revoked { get; set; }
        public DateTime? Expired { get; set; }

        public virtual Account? Account { get; set; }
    }
}
