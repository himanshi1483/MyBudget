namespace MyBudget.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class init0011 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SubCategories", "Type", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.SubCategories", "Type");
        }
    }
}
