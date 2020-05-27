namespace EP.EntityData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class FeeAndHorizontalPoster : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tournament", "Fee", c => c.Double(nullable: false));
            AddColumn("dbo.Tournament", "HorizontalPoster", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tournament", "HorizontalPoster");
            DropColumn("dbo.Tournament", "Fee");
        }
    }
}
