using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RHBGame.Data.Models
{
    public class Question
    {
        [Column("question_id")]
        public int QuestionId { get; set; }
        [Column("question")]
        public string QuestionText { get; set; }


        public Question()
        {

        }


        public Question(int questionId, string questionText)
        {
            QuestionId = questionId;
            QuestionText = questionText;
        }
    }
}