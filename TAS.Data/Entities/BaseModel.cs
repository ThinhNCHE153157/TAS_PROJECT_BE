using System.ComponentModel.DataAnnotations;
using TAS.Data.Entities.Interfaces;

namespace TAS.Data.Entities
{
    public class BaseModel : IDateTracking, ISoftDateTracking
    {
        [Required]
        public bool IsDelete { get; set; }
        [Required]
        [MaxLength(20)]
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        [Required]
        [MaxLength(20)]
        public string UpdateUser { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
