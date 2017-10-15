using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHBGame.Data.Models
{
    [Table("Answers")]
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column("answer_id")]
        public Int32 Id { get; set; }
        
        [Required]
        [MaxLength(3), MinLength(2)]
        [Column("answer")]
        public String Text { get; set; }
        
        [Required]
        [Column("question_id")]
        public Int32 QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }
        
        [Required]
        [Column("player_id")]
        public Int32 PlayerId { get; set; }

        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }
        
        [Column("created", TypeName = "datetime2")]
        public DateTime Created { get; set; }
    }
}