using OneC.EntityData.Context;
using System.Data.Entity.Migrations;

namespace OneC.EntityData.Migrations
{   
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataContext context)
        {
        }
    }
}
