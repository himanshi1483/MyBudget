namespace MyBudget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.BankAccounts",
            //    c => new
            //        {
            //            AccountId = c.Int(nullable: false, identity: true),
            //            BankName = c.String(),
            //            AccountName = c.String(),
            //            AccountNumber = c.String(),
            //            AccountType = c.Int(nullable: false),
            //            Balance = c.Double(nullable: false),
            //        })
            //    .PrimaryKey(t => t.AccountId);
            
            //CreateTable(
            //    "dbo.Categories",
            //    c => new
            //        {
            //            CategoryId = c.Int(nullable: false, identity: true),
            //            CategoryName = c.String(),
            //        })
            //    .PrimaryKey(t => t.CategoryId);
            
            //CreateTable(
            //    "dbo.ExpenseDetails",
            //    c => new
            //        {
            //            ExpenseId = c.Int(nullable: false, identity: true),
            //            ActualAmount = c.Double(nullable: false),
            //            SubCategoryId = c.Int(nullable: false),
            //            MonthsPassed = c.Int(nullable: false),
            //            DebitDate = c.DateTime(nullable: false),
            //            ForMonth = c.Int(nullable: false),
            //            FinancialYear = c.String(),
            //        })
            //    .PrimaryKey(t => t.ExpenseId);
            
            //CreateTable(
            //    "dbo.IncomeDetails",
            //    c => new
            //        {
            //            IncomeId = c.Int(nullable: false, identity: true),
            //            ActualAmount = c.Double(nullable: false),
            //            SubCategoryId = c.Int(nullable: false),
            //            CreditDate = c.DateTime(nullable: false),
            //            ForMonth = c.Int(nullable: false),
            //            FinancialYear = c.String(),
            //        })
            //    .PrimaryKey(t => t.IncomeId);
            
            //CreateTable(
            //    "dbo.InvestmentDetails",
            //    c => new
            //        {
            //            InvestmentId = c.Int(nullable: false, identity: true),
            //            ActualAmount = c.Double(nullable: false),
            //            SubCategoryId = c.Int(nullable: false),
            //            MonthsPassed = c.Int(nullable: false),
            //            AmountAccumulated = c.Double(nullable: false),
            //            DebitDate = c.DateTime(nullable: false),
            //            ForMonth = c.Int(nullable: false),
            //            FinancialYear = c.String(),
            //        })
            //    .PrimaryKey(t => t.InvestmentId);
            
            //CreateTable(
            //    "dbo.MonthlyPlans",
            //    c => new
            //        {
            //            PlanId = c.Int(nullable: false, identity: true),
            //            CreatedOn = c.DateTime(nullable: false),
            //            ForMonth = c.Int(nullable: false),
            //            FinancialYear = c.String(),
            //        })
            //    .PrimaryKey(t => t.PlanId);
            
            //CreateTable(
            //    "dbo.AspNetRoles",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            Name = c.String(nullable: false, maxLength: 256),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            //CreateTable(
            //    "dbo.AspNetUserRoles",
            //    c => new
            //        {
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            RoleId = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => new { t.UserId, t.RoleId })
            //    .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.RoleId);
            
            //CreateTable(
            //    "dbo.SavingsDetails",
            //    c => new
            //        {
            //            SavingsId = c.Int(nullable: false, identity: true),
            //            ActualAmount = c.Double(nullable: false),
            //            SubCategoryId = c.Int(nullable: false),
            //            Date = c.DateTime(nullable: false),
            //            MonthsPassed = c.Int(nullable: false),
            //            AmountAccumulated = c.Double(nullable: false),
            //            ForMonth = c.Int(nullable: false),
            //            FinancialYear = c.String(),
            //        })
            //    .PrimaryKey(t => t.SavingsId);
            
            //CreateTable(
            //    "dbo.SMSDatas",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            SMSText = c.String(),
            //            Date = c.DateTime(),
            //            BankName = c.String(),
            //            TransactionType = c.String(),
            //            Amount = c.String(),
            //            Label = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.SubCategories",
            //    c => new
            //        {
            //            SubCategoryId = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            Frequency = c.Int(nullable: false),
            //            ExpectedAmount = c.Double(nullable: false),
            //            StartDate = c.DateTime(),
            //            EndDate = c.DateTime(),
            //            ParentCategoryId = c.Int(nullable: false),
            //            Owner = c.String(),
            //            IsDefault = c.Boolean(nullable: false),
            //        })
            //    .PrimaryKey(t => t.SubCategoryId);
            
            //CreateTable(
            //    "dbo.AspNetUsers",
            //    c => new
            //        {
            //            Id = c.String(nullable: false, maxLength: 128),
            //            Email = c.String(maxLength: 256),
            //            EmailConfirmed = c.Boolean(nullable: false),
            //            PasswordHash = c.String(),
            //            SecurityStamp = c.String(),
            //            PhoneNumber = c.String(),
            //            PhoneNumberConfirmed = c.Boolean(nullable: false),
            //            TwoFactorEnabled = c.Boolean(nullable: false),
            //            LockoutEndDateUtc = c.DateTime(),
            //            LockoutEnabled = c.Boolean(nullable: false),
            //            AccessFailedCount = c.Int(nullable: false),
            //            UserName = c.String(nullable: false, maxLength: 256),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            //CreateTable(
            //    "dbo.AspNetUserClaims",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            ClaimType = c.String(),
            //            ClaimValue = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.AspNetUserLogins",
            //    c => new
            //        {
            //            LoginProvider = c.String(nullable: false, maxLength: 128),
            //            ProviderKey = c.String(nullable: false, maxLength: 128),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //        })
            //    .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
            //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            //DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            //DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            //DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            //DropIndex("dbo.AspNetUsers", "UserNameIndex");
            //DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            //DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            //DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            //DropTable("dbo.AspNetUserLogins");
            //DropTable("dbo.AspNetUserClaims");
            //DropTable("dbo.AspNetUsers");
            //DropTable("dbo.SubCategories");
            //DropTable("dbo.SMSDatas");
            //DropTable("dbo.SavingsDetails");
            //DropTable("dbo.AspNetUserRoles");
            //DropTable("dbo.AspNetRoles");
            //DropTable("dbo.MonthlyPlans");
            //DropTable("dbo.InvestmentDetails");
            //DropTable("dbo.IncomeDetails");
            //DropTable("dbo.ExpenseDetails");
            //DropTable("dbo.Categories");
            //DropTable("dbo.BankAccounts");
        }
    }
}
