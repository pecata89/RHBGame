using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RHBGame.WebApi.Controllers
{
    public partial class AnswerController
    {
        public class ListParams
        {
            [Required]
            [JsonProperty("authToken")]
            public String AuthToken { get; set; }
        }

        public class CreateParams
        {
            [Required]
            [JsonProperty("authToken")]
            public String AuthToken { get; set; }
        }
    }
}