using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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
        [JsonIgnore]
        [Column("username")]
        public String Username { get; set; }

        [JsonIgnore]
        [Required]
        [MaxLength(50)]
        [Column("password_hash")]
        public Byte[] PasswordHash { get; set; }

        [JsonIgnore]
        [Required]
        [MaxLength(50)]
        [Column("password_salt")]
        public Byte[] PasswordSalt { get; set; }

        [Required]
        [MaxLength(100)]
        [JsonIgnore]
        [Column("email")]
        public String Email { get; set; }

        [Required]
        [MaxLength(6)]
        [Column("gender")]
        public String Gender { get; set; }

        [Required]
        [Column("birthdate", TypeName = "date")]
        public DateTime Birthdate { get; set; }

        [JsonIgnore]
        [Column("edited", TypeName = "datetime2")]
        public DateTime? Edited { get; set; }

        [Required]
        [JsonIgnore]
        [Column("created", TypeName = "datetime2")]
        public DateTime Created { get; set; }
    }
}