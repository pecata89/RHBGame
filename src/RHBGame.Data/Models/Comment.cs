using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHBGame.Data.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column("comment_id")]
        public Int32 Id { get; set; }

        [Required]
        [MaxLength(500)]
        [Column("comment")]
        public String Text { get; set; }
        
        [Required]
        [Column("answer_id")]
        public Int32 AnswerId { get; set; }

        [ForeignKey(nameof(AnswerId))]
        public Answer Answer { get; set; }
        
        [Required]
        [Column("player_id")]
        public Int32 PlayerId { get; set; }

        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }
  
        [Column("created", TypeName = "datetime2")]
        public DateTime Created { get; set; }
    }
}