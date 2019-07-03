namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseDetails",
                c => new
                    {
                        ExpenseId = c.Int(nullable: false, identity: true),
                        ActualAmount = c.Double(nullable: false),
                        SubCategoryId = c.Int(nullable: false),
                        DebitDate = c.DateTime(nullable: false),
                        ForMonth = c.String(),
                        FinancialYear = c.String(),
                    })
                .PrimaryKey(t => t.ExpenseId);
            
            CreateTable(
                "dbo.InvestmentDetails",
                c => new
                    {
                        InvestmentId = c.Int(nullable: false, identity: true),
                        ActualAmount = c.Double(nullable: false),
                        SubCategoryId = c.Int(nullable: false),
                        DebitDate = c.DateTime(nullable: false),
                        ForMonth = c.String(),
                        FinancialYear = c.String(),
                    })
                .PrimaryKey(t => t.InvestmentId);
            
            CreateTable(
                "dbo.SavingsDetails",
                c => new
                    {
                        SavingsId = c.Int(nullable: false, identity: true),
                        ActualAmount = c.Double(nullable: false),
                        SubCategoryId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        ForMonth = c.String(),
                        FinancialYear = c.String(),
                    })
                .PrimaryKey(t => t.SavingsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SavingsDetails");
            DropTable("dbo.InvestmentDetails");
            DropTable("dbo.ExpenseDetails");
        }
    }
}
