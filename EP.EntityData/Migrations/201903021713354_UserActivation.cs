namespace EP.EntityData.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UserActivation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActivationCode",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Code = c.String(),
                        SendingDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ActivationCode");
        }
    }
}
