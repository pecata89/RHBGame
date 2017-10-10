using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RHBGame.Data.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public int AnswerId { get; set; }
        public DateTime Created { get; set; }


        public Comment(int commentId, string commentText, int answerId, DateTime created)
        {
            CommentId = commentId;
            CommentText = commentText;
            AnswerId = answerId;
            Created = created;
        }
    }
}