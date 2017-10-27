using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace RHBGame.Data.Models
{
    [Table("Sessions")]
    public class Session
    {
        [Key]
        [Required]
        [MinLength(28), MaxLength(28)]
        [Column("session_token", TypeName = "char")]
        public String Token { get; set; }

        [Required]
        [Column("player_id")]
        public Int32 PlayerId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }

        [Required]
        [JsonIgnore]
        [Column("last_activity", TypeName = "datetime2")]
        public DateTime LastActivity { get; set; }

        [Required]
        [JsonIgnore]
        [Column("created", TypeName = "datetime2")]
        public DateTime Created { get; set; }
    }
}
