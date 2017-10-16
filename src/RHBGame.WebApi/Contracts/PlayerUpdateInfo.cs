using System;
using System.ComponentModel.DataAnnotations;
using RHBGame.Data.Models;

namespace RHBGame.WebApi.Contracts
{
    // The information that is passed from the web client that is needed to edit the player.
    // For now we will allow only edits for the second level information.
    // Login info like username and password, and retrive password info like email will be implemented later.
    public class PlayerUpdateInfo
    {
        /*
        [Required]
        public Session Session { get; set; }


        [Required]
        public Player Player { get; set; }
        */

        [Required]
        public String AuthToken { get; set; }


        [Required]
        public Int32 PlayerId { get; set; }


        [Required]
        public String Name { get; set; }


        [Required]
        public String Gender { get; set; }


        [Required]
        public DateTime Birthdate { get; set; }
    }
}