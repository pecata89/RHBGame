using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHBGame.Data.Models
{
    public class Question
    {
        [Column("question_id")]
        public int QuestionId { get; set; }

        [Column("question")]
        public string QuestionText { get; set; }

        [Required]
        public Topic Topic { get; set; }
    }
}