using System.Data.Entity.Migrations;

namespace RHBGame.Data.Migrations
{
    public sealed class V001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TopicQuestions",
                c => new
                {
                    topic_id = c.Int(),
                    question_id = c.Int(),
                })
                .PrimaryKey(k => new { k.topic_id, k.question_id })
                .ForeignKey("dbo.Topics", t => t.topic_id)
                .ForeignKey("dbo.Questions", t => t.question_id)
                .Index(k => k.topic_id)
                .Index(k => k.question_id);
        }
    }
}
