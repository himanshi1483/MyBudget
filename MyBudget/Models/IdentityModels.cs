﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyBudget.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<MyBudget.Models.Categories> Categories { get; set; }

        public System.Data.Entity.DbSet<MyBudget.Models.SubCategories> SubCategories { get; set; }

        public System.Data.Entity.DbSet<MyBudget.Models.IncomeDetail> IncomeDetails { get; set; }

        public System.Data.Entity.DbSet<MyBudget.Models.ExpenseDetail> ExpenseDetails { get; set; }

        public System.Data.Entity.DbSet<MyBudget.Models.SavingsDetail> SavingsDetails { get; set; }

        public System.Data.Entity.DbSet<MyBudget.Models.InvestmentDetail> InvestmentDetails { get; set; }

        public System.Data.Entity.DbSet<MyBudget.Models.BankAccounts> BankAccounts { get; set; }
        public System.Data.Entity.DbSet<MyBudget.Models.MonthlyPlan> MonthlyPlans { get; set; }
        public System.Data.Entity.DbSet<MyBudget.Models.SMSData> SMSData { get; set; }
    }
}