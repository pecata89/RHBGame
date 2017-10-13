using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHBGame.Data.Models
{
    [Table("Players")]
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column("player_id")]
        public Int32 Id { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("name")]
        public String Name { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("username")]
        public String Username { get; set; }

        [Column("password_hash")]
        public Byte[] PasswordHash { get; set; }

        [Column("password_salt")]
        public Byte[] PasswordSalt { get; set; }

        [Required]
        [Column("email")]
        public String Email { get; set; }

        [Required]
        [Column("gender")]
        public String Gender { get; set; }

        [Required]
        [Column("birthdate")]
        public DateTime Birthdate { get; set; }

        [Column("edited")]
        public DateTime? Edited { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }
    }
}