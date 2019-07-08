namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpenseDetails", "MonthsPassed", c => c.Int(nullable: false));
            AddColumn("dbo.InvestmentDetails", "MonthsPassed", c => c.Int(nullable: false));
            AddColumn("dbo.InvestmentDetails", "AmountAccumulated", c => c.Double(nullable: false));
            AddColumn("dbo.SavingsDetails", "MonthsPassed", c => c.Int(nullable: false));
            AddColumn("dbo.SavingsDetails", "AmountAccumulated", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SavingsDetails", "AmountAccumulated");
            DropColumn("dbo.SavingsDetails", "MonthsPassed");
            DropColumn("dbo.InvestmentDetails", "AmountAccumulated");
            DropColumn("dbo.InvestmentDetails", "MonthsPassed");
            DropColumn("dbo.ExpenseDetails", "MonthsPassed");
        }
    }
}
