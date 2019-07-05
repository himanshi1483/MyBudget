namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SMSDatas", "BankName", c => c.String());
            AddColumn("dbo.SMSDatas", "TransactionType", c => c.String());
            AddColumn("dbo.SMSDatas", "Amount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SMSDatas", "Amount");
            DropColumn("dbo.SMSDatas", "TransactionType");
            DropColumn("dbo.SMSDatas", "BankName");
        }
    }
}
