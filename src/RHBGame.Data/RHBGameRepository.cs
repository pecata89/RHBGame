using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using RHBGame.Data.Models;

namespace RHBGame.Data
{
    public class RHBGameRepository : DbContext
    {
        static RHBGameRepository()
        {
            // Setup migrations
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RHBGameRepository, RHBGameMigrationConfiguration>("Database"));
        }


        public RHBGameRepository() : base("name=Database")
        {
        }


        public DbSet<Player> Players { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Session> Sessions { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Do not allow cascade deletes
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Topic>()
                .HasMany(t => t.Questions)
                .WithMany(q => q.Topics)
                .Map(cs =>
                {
                    cs.MapLeftKey("topic_id");
                    cs.MapRightKey("question_id");
                    cs.ToTable("TopicQuestions");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}