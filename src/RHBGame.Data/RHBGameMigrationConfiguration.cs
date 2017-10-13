using System.Data.Entity.Migrations;

namespace RHBGame.Data
{
    public sealed class RHBGameMigrationConfiguration : DbMigrationsConfiguration<RHBGameRepository>
    {
        public RHBGameMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsAssembly = typeof(RHBGameMigrationConfiguration).Assembly;
        }


        protected override void Seed(RHBGameRepository context)
        {
            base.Seed(context);
        }
    }
}
