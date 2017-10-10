using System.Data.Entity;
using RHBGame.Data.Models;

namespace RHBGame.Data
{
    public class RHBGameRepository : DbContext
    {
        public RHBGameRepository() : base("RHBGameContext")
        {

        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}
    }
}