using System.ComponentModel.DataAnnotations;

namespace TAS.Data.Dtos.Requests
{
    public class CreateTestRequestDto
    {
        [Required]
        [StringLength(50)]
        public string? TestName { get; set; }
        [Required]
        [Range(0, 300)]
        public double? TestDuration { get; set; }
        [Required]
        [Range(0, 300)]
        public double? TestTotalScore { get; set; }
        [Required]
        [StringLength(500)]
        public string? TestDescription { get; set; }
    }
}
