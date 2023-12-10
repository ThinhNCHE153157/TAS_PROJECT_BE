using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.Dtos.Responses
{
    public class GetQuestionByTestIdResponseDto
    {
        public int QuestionId { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Type { get; set; }
        public string? Note { get; set; }
        public string? ResultA { get; set; }
        public string? ResultB { get; set; }
        public string? ResultC { get; set; }
        public string? ResultD { get; set; }
        public string? CorrectResult { get; set; }

        public GetQuestionByTestIdResponseDto(int questionId, string? description, string? image, string? type, string? note, string? resultA, string? resultB, string? resultC, string? resultD, string? correctResult)
        {
            QuestionId = questionId;
            Description = description;
            Image = image;
            Type = type;
            Note = note;
            ResultA = resultA;
            ResultB = resultB;
            ResultC = resultC;
            ResultD = resultD;
            CorrectResult = correctResult;
        }
    }
}
