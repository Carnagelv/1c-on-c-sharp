namespace OneC.EntityData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TableColumns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TableItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TableColumnId = c.Int(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TableColumns", t => t.TableColumnId, cascadeDelete: true)
                .Index(t => t.TableColumnId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TableItems", "TableColumnId", "dbo.TableColumns");
            DropIndex("dbo.TableItems", new[] { "TableColumnId" });
            DropTable("dbo.TableItems");
            DropTable("dbo.TableColumns");
        }
    }
}
