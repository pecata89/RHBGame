using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHBGame.Data.Models
{
    [Table("Sessions")]
    public class Session
    {
        [Key]
        [Required]
        [MaxLength(28)]
        [Column("session_token", TypeName = "char")]
        public String Token { get; set; }

        [Required]
        [Column("created", TypeName = "datetime2")]
        public DateTime Created { get; set; }
        
        [Required]
        [Column("last_activity", TypeName = "datetime2")]
        public DateTime LastActivity { get; set; }
        
        [Required]
        [Column("player_id")]
        public Int32 PlayerId { get; set; }

        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }
    }
}
