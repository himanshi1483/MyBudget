namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initnew : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SubCategories", "Type");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SubCategories", "Type", c => c.Int(nullable: false));
        }
    }
}
