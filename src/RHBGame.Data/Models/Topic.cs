using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RHBGame.Data.Models
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }


        public Topic(int topicId, string name, string color)
        {
            TopicId = topicId;
            Name = name;
            Color = color;
        }
    }
}