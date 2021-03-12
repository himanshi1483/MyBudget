namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubCategories", "PortfolioNumber", c => c.String());
            AddColumn("dbo.SubCategories", "ExpectedInterest", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.SubCategories", "TypeOfInvestment", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubCategories", "TypeOfInvestment");
            DropColumn("dbo.SubCategories", "ExpectedInterest");
            DropColumn("dbo.SubCategories", "PortfolioNumber");
        }
    }
}
