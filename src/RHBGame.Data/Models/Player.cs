using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RHBGame.Data.Models
{
    [Table("Players")]
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("player_id")]
        public int PlayerId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        [Column("birthdate")]
        public DateTime Birthdate { get; set; }

        [Column("edited")]
        public DateTime? Edited { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }



        // To make one auto-implemented property with fields.


        public Player()
        {

        }


        public Player(int playerId, string name, string username, string password, string email, string gender, DateTime birthdate, DateTime edited, DateTime created)
        {
            PlayerId = playerId;
            Name = name;
            Username = username;
            Password = password;
            Email = email;
            Gender = gender;
            Birthdate = birthdate;
            Edited = edited;
            Created = created;
        }
    }
}