using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MyBudget.Utility
{
    public class Enumerations
    {
        public enum Month
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }

        public enum Frequency
        {
            Once,
            Weekly,
            Monthly,
            Yearly
        }

        public enum AccountType
        {
            Savings,
            PPF,
            LoanAccount,
            CreditCard
        }

        public enum Type
        {
            NA = 0,
            [Display(Name = "Tax Saving Fixed Deposit")]
            TaxSavingFD = 1,
            [Display(Name = "Regular Fixed Deposit")]
            RegularFD = 2,
            [Display(Name = "Tax Saving Mutual Fund")]
            TaxSavingMF = 3,
            [Display(Name = "Regular Mutual Fund")]
            RegularMF = 4
        }
    }


    //public static class EnumExtension
    //{
    //    public static string GetDescription(this Enum GenericEnum)
    //    {
    //        Type genericEnumType = GenericEnum.GetType();
    //        MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
    //        if ((memberInfo != null && memberInfo.Length > 0))
    //        {
    //            var _Attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
    //            if ((_Attribs != null && _Attribs.Count() > 0))
    //            {
    //                return ((System.ComponentModel.DescriptionAttribute)_Attribs.ElementAt(0)).Description;
    //            }
    //        }
    //        return GenericEnum.ToString();
    //    }

    //    public static string GetDescriptionVal(Enum value)
    //    {
    //        return
    //            value
    //                .GetType()
    //                .GetMember(value.ToString())
    //                .FirstOrDefault()
    //                ?.GetCustomAttribute<System.ComponentModel.DescriptionAttribute>()
    //                ?.Description
    //            ?? value.ToString();
    //    }



    //}

}