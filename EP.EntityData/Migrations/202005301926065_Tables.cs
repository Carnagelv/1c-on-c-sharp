namespace OneC.EntityData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Tables : DbMigration
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
                        IsInitial = c.Boolean(nullable: false),
                        TableId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tables", t => t.TableId, cascadeDelete: true)
                .Index(t => t.TableId);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InitialColumnName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TableRows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TableColumnId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TableColumns", t => t.TableColumnId, cascadeDelete: true)
                .Index(t => t.TableColumnId);
            
            CreateTable(
                "dbo.TableRowItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TableRowId = c.Int(nullable: false),
                        TableColumnId = c.Int(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TableRows", t => t.TableRowId, cascadeDelete: true)
                .Index(t => t.TableRowId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TableRowItems", "TableRowId", "dbo.TableRows");
            DropForeignKey("dbo.TableRows", "TableColumnId", "dbo.TableColumns");
            DropForeignKey("dbo.TableColumns", "TableId", "dbo.Tables");
            DropIndex("dbo.TableRowItems", new[] { "TableRowId" });
            DropIndex("dbo.TableRows", new[] { "TableColumnId" });
            DropIndex("dbo.TableColumns", new[] { "TableId" });
            DropTable("dbo.TableRowItems");
            DropTable("dbo.TableRows");
            DropTable("dbo.Tables");
            DropTable("dbo.TableColumns");
        }
    }
}
