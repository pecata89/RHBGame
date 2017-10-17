using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace RHBGame.Data.Models
{
    [Table("Questions")]
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

        [Required]
        [JsonIgnore]
        [Column("topic_id")]
        public Int32 TopicId { get; }

        [JsonIgnore]
        [ForeignKey(nameof(TopicId))]
        public virtual ICollection<Topic> Topics { get; set; }
    }
}