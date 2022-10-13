namespace MyBudget.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class init5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvestmentDetails", "DepositType", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.InvestmentDetails", "DepositType");
        }
    }
}
