using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RHBGame.WebApi.Controllers
{
    public partial class QuestionController
    {
        public class ListParams
        {
            [Required]
            [JsonProperty("authToken")]
            public String AuthToken { get; set; }
        }

        public class FindByIdParams
        {
            [Required]
            [JsonProperty("authToken")]
            public String AuthToken { get; set; }


            [Required]
            [JsonProperty("questionId")]
            public Int32 QuestionId { get; set; }
        }

        public class FindByTopicParams
        {
            [Required]
            [JsonProperty("authToken")]
            public String AuthToken { get; set; }

            
            [Required]
            [JsonProperty("topicId")]
            public Int32 TopicId { get; set; }
        }
    }
}