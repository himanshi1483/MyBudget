namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ExpenseDetails", "ForMonth", c => c.Int(nullable: false));
            AlterColumn("dbo.IncomeDetails", "ForMonth", c => c.Int(nullable: false));
            AlterColumn("dbo.InvestmentDetails", "ForMonth", c => c.Int(nullable: false));
            AlterColumn("dbo.SavingsDetails", "ForMonth", c => c.Int(nullable: false));
            AlterColumn("dbo.SubCategories", "Frequency", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SubCategories", "Frequency", c => c.String());
            AlterColumn("dbo.SavingsDetails", "ForMonth", c => c.String());
            AlterColumn("dbo.InvestmentDetails", "ForMonth", c => c.String());
            AlterColumn("dbo.IncomeDetails", "ForMonth", c => c.String());
            AlterColumn("dbo.ExpenseDetails", "ForMonth", c => c.String());
        }
    }
}
