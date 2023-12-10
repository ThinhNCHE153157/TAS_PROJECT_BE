namespace TAS.Data.Entities
{
    public partial class Enterprise
    {
        public string EnterpriseCode { get; set; } = null!;
        public string EnterpriseName { get; set; } = null!;
        public string? ForeignName { get; set; }
        public string ShortName { get; set; } = null!;
        public string RepresentativeName { get; set; } = null!;
        public string OfficeAddress { get; set; } = null!;
        public int? AccountId { get; set; }

        public virtual Account? Account { get; set; }
    }
}
