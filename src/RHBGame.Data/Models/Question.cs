using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHBGame.Data.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column("question_id")]
        public Int32 Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("question")]
        public String Text { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }
}