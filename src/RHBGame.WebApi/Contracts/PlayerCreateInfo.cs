using System;
using System.ComponentModel.DataAnnotations;

namespace RHBGame.WebApi.Contracts
{
    /// <summary>
    /// The information passed from the web client that is needed to create a new player.
    /// </summary>
    public sealed class PlayerCreateInfo
    {
        [Required]
        public String Name { get; set; }


        [Required]
        public String Password { get; set; }


        [Required]
        public String Username { get; set; }


        [Required]
        public String Email { get; set; }


        [Required]
        public String Gender { get; set; }


        [Required]
        public DateTime BirthDate { get; set; }
    }
}