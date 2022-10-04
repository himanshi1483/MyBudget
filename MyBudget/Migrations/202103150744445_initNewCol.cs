namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initNewCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubCategories", "ExpectedMonthlyAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubCategories", "ExpectedMonthlyAmount");
        }
    }
}
