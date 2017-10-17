using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RHBGame.WebApi.Controllers
{
    public partial class CommentController
    {
        public class FindByAnswerParams
        {
            [Required]
            [JsonProperty("authToken")]
            public String AuthToken { get; set; }

            [Required]
            [JsonProperty("answerId")]
            public Int32 AnswerId { get; set; }
        }

        public class CreateParams
        {
            [Required]
            [JsonProperty("authToken")]
            public String AuthToken { get; set; }

            [Required]
            [JsonProperty("comment")]
            public String Comment { get; set; }

            [Required]
            [JsonProperty("playerId")]
            public Int32 PlayerId { get; set; }

            [Required]
            [JsonProperty("answerId")]
            public Int32 AnswerId { get; set; }
        }

        public class UpdateParams
        {
            [Required]
            [JsonProperty("authToken")]
            public String AuthToken { get; set; }

            [Required]
            [JsonProperty("commentId")]
            public Int32 CommentId { get; set; }

            [Required]
            [JsonProperty("comment")]
            public String Comment { get; set; }
        }
        public class FindByIdParams
        {
            [Required]
            [JsonProperty("authToken")]
            public String AuthToken { get; set; }

            [Required]
            [JsonProperty("commentId")]
            public Int32 CommentId { get; set; }
        }

        //public class RemoveParams
        //{
        //    [Required]
        //    [JsonProperty("authToken")]
        //    public String AuthToken { get; set; }

        //    [Required]
        //    [JsonProperty("commentId")]
        //    public Int32 CommentId { get; set; }
        //}
    }
}
