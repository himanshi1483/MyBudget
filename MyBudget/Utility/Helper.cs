using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MyBudget.Utility
{
    public static class Helper
    {
       public static string FormatNumber(double num)
        {
            if (num >= 100000)
                return FormatNumber(num / 1000) + "K";
            if (num >= 10000)
            {
                return (num / 1000D).ToString("0.#") + "K";
            }
            return num.ToString("#,0");
        }
    }
}