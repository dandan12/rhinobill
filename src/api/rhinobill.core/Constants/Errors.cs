using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rhinobill.core.Constants
{
    public static class Errors
    {
        public static string MinMaxLengthCode = "MinMaxLengthCode";
        public static string MinMaxLength230Message = "Length cannot be less than 2 or greater than 30";
        public static string MinMaxLength510Message = "Length cannot be less than 5 or greater than 10";
        public static string MinMaxLength550Message = "Length cannot be less than 5 or greater than 50";

        public static string CreditsMoreThanZeroCode = "CreditsMoreThanZero";
        public static string CreditsMoreThanZeroMessage = "Credits should be greater than zero";
    }
}
