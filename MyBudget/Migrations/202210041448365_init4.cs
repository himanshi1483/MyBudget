namespace MyBudget.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.LoanDetails",
            //    c => new
            //        {
            //            LoanId = c.Int(nullable: false, identity: true),
            //            ActualAmount = c.Double(nullable: false),
            //            SubCategoryId = c.Int(nullable: false),
            //            MonthsPassed = c.Int(nullable: false),
            //            AmountAccumulated = c.Double(nullable: false),
            //            DebitDate = c.DateTime(nullable: false),
            //            ForMonth = c.Int(nullable: false),
            //            FinancialYear = c.String(),
            //        })
            //    .PrimaryKey(t => t.LoanId);

            //AddColumn("dbo.SubCategories", "InvestedAmount", c => c.Double(nullable: false));
            //AddColumn("dbo.SubCategories", "ExpectedMonthlyAmount", c => c.Double(nullable: false));
            //AddColumn("dbo.SubCategories", "PortfolioNumber", c => c.String());
            //AddColumn("dbo.SubCategories", "ExpectedInterest", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }

        public override void Down()
        {
            //DropColumn("dbo.SubCategories", "ExpectedInterest");
            //DropColumn("dbo.SubCategories", "PortfolioNumber");
            //DropColumn("dbo.SubCategories", "ExpectedMonthlyAmount");
            //DropColumn("dbo.SubCategories", "InvestedAmount");
            //DropTable("dbo.LoanDetails");
        }
    }
}
