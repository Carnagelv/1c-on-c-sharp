namespace EP.EntityData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RussianNews : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "TextRu", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "TextRu");
        }
    }
}
