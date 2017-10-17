using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RHBGame.WebApi.Controllers
{
    public partial class TopicController
    {
        public class FindByIdParams
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