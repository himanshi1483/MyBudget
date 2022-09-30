namespace MyBudget.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class init001 : DbMigration
    {
        public override void Up()
        {
            // AddColumn("dbo.SubCategories", "Type", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            // DropColumn("dbo.SubCategories", "Type");
        }
    }
}
