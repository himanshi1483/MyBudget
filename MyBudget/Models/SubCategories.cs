using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static MyBudget.Utility.Enumerations;

namespace MyBudget.Models
{
    public class SubCategories
    {
        [Key]
        public int SubCategoryId { get; set; }
        public string Name { get; set; }
        public Frequency Frequency { get; set; }
        public double ExpectedAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int ParentCategoryId { get; set; }
        [NotMapped]
        public string ParentCategoryName { get; set; }
        public string Owner { get; set; }
        public bool IsDefault { get; set; }
    }
}