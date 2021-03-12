namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newFields2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubCategories", "InvestedAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubCategories", "InvestedAmount");
        }
    }
}
