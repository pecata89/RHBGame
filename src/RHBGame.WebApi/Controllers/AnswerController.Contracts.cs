using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RHBGame.WebApi.Controllers
{
    public partial class AnswerController
    {
        public class FindByQuestionParams
        {
            [Required]
            [JsonProperty("authToken")]
            public String AuthToken { get; set; }

            [Required]
            [JsonProperty("questionId")]
            public Int32 QuestionId { get; set; }
        }

        public class CreateParams
        {
            [Required]
            [JsonProperty("authToken")]
            public String AuthToken { get; set; }

            [Required]
            [JsonProperty("answer")]
            public String Answer { get; set; }

            [Required]
            [JsonProperty("playerId")]
            public Int32 PlayerId { get; set; }

            [Required]
            [JsonProperty("questionId")]
            public Int32 QuestionId { get; set; }
        }
    }
}