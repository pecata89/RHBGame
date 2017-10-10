using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RHBGame.Data.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public int QuestionId { get; set; }
        public int PlayerId { get; set; }
        public DateTime Created { get; set; }


        public Answer(int answerId, string answerText, int questionId, int playerId, DateTime created)
        {
            AnswerId = answerId;
            AnswerText = answerText;
            QuestionId = questionId;
            PlayerId = playerId;
            Created = created;
        }
    }
}