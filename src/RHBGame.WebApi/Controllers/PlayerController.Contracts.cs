using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RHBGame.WebApi.Controllers
{
    public partial class PlayerController
    {
        public class LoginParams
        {
            [Required]
            [JsonProperty("username")]
            public String Username { get; set; }


            [Required]
            [JsonProperty("password")]
            public String Password { get; set; }
        }

        public class UpdateParams
        {
            [Required]
            [JsonProperty("authToken")]
            public String AuthToken { get; set; }


            [Required]
            [JsonProperty("playerId")]
            public Int32 PlayerId { get; set; }


            [Required]
            [JsonProperty("name")]
            public String Name { get; set; }


            [Required]
            [JsonProperty("gender")]
            public String Gender { get; set; }


            [Required]
            [JsonProperty("birthdate")]
            public DateTime Birthdate { get; set; }
        }

        public class CreateParams
        {
            [Required]
            [JsonProperty("name")]
            public String Name { get; set; }


            [Required]
            [JsonProperty("username")]
            public String Username { get; set; }


            [Required]
            [JsonProperty("password")]
            public String Password { get; set; }


            [Required]
            [JsonProperty("email")]
            public String Email { get; set; }


            [Required]
            [JsonProperty("gender")]
            public String Gender { get; set; }


            [Required]
            [JsonProperty("birthdate")]
            public DateTime Birthdate { get; set; }
        }
    }
}