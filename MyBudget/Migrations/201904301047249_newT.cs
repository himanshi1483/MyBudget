namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MonthlyPlans",
                c => new
                    {
                        PlanId = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        ForMonth = c.Int(nullable: false),
                        FinancialYear = c.String(),
                    })
                .PrimaryKey(t => t.PlanId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MonthlyPlans");
        }
    }
}
