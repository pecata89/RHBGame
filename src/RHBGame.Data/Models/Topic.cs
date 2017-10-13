using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHBGame.Data.Models
{
    public class Topic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column("topic_id")]
        public Int32 Id { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("name")]
        public String Name { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("color")]
        public String Color { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}