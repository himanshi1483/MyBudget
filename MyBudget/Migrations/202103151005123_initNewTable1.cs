namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initNewTable1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoanDetails",
                c => new
                    {
                        LoanId = c.Int(nullable: false, identity: true),
                        ActualAmount = c.Double(nullable: false),
                        SubCategoryId = c.Int(nullable: false),
                        MonthsPassed = c.Int(nullable: false),
                        AmountAccumulated = c.Double(nullable: false),
                        DebitDate = c.DateTime(nullable: false),
                        ForMonth = c.Int(nullable: false),
                        FinancialYear = c.String(),
                    })
                .PrimaryKey(t => t.LoanId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoanDetails");
        }
    }
}
