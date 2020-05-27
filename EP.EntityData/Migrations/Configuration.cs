using EP.EntityData.Context;
using System.Data.Entity.Migrations;

namespace EP.EntityData.Migrations
{   
    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataContext context)
        {
            context.Webpages_Roles.AddOrUpdate(m => m.RoleId,  
                new Webpages_Roles() { RoleName = "SuperAdmin" },
                new Webpages_Roles() { RoleName = "SportsMan" },
                new Webpages_Roles() { RoleName = "CyberSportsMan" });
        }
    }
}
