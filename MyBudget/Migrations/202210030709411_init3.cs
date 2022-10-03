namespace MyBudget.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            //AlterColumn("dbo.SubCategories", "DepositType", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            // AlterColumn("dbo.SubCategories", "DepositType", c => c.String());
        }
    }
}
