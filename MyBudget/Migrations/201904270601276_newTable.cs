namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        BankName = c.String(),
                        AccountName = c.String(),
                        AccountNumber = c.String(),
                        AccountType = c.Int(nullable: false),
                        Balance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BankAccounts");
        }
    }
}
