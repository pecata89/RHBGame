using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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
        [JsonIgnore]
        [Column("question_id")]
        public Int32 QuestionId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }
        
        [Required]
        [JsonIgnore]
        [Column("player_id")]
        public Int32 PlayerId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(PlayerId))]
        public Player Player { get; set; }

        [JsonIgnore]
        [Column("created", TypeName = "datetime2")]
        public DateTime Created { get; set; }

        [JsonIgnore]
        public virtual ICollection<Comment> Comments { get; }
    }
}