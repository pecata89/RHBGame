using System;
using System.ComponentModel.DataAnnotations;
using RHBGame.Data.Models;

namespace RHBGame.WebApi.Contracts
{
    public class PlayerLoginInfo
    {
        [Required]
        public String Username { get; set; }


        [Required]
        public String Password { get; set; }
    }
}