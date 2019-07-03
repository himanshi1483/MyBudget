namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SMSDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SMSText = c.String(),
                        Date = c.DateTime(nullable: false),
                        Label = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SMSDatas");
        }
    }
}
