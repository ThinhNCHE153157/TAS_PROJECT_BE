namespace TAS.Application.Services.Interfaces
{
    public class QuestionHomepageResponeDto
    {
        public int? TestId { get; set; }
        public int QuestionId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Type { get; set; }
        public string? Note { get; set; }
    }
}