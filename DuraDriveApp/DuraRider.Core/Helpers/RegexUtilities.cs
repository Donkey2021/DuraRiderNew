using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DuraRider.Core.Helpers
{
    public class RegexUtilities
    {
        public static bool EmailValidation(string Email)
        {
            if (string.IsNullOrEmpty(Email))
                return false;
            var regex = new Regex(AppConstant.EmailRegex, RegexOptions.IgnoreCase);
            var isMatch = regex.IsMatch(Email);
            return isMatch;

        }
        public static bool PhoneNumberValidation(string PhoneNumber)
        {
            if (string.IsNullOrEmpty(PhoneNumber))
                return false;
            if (5 <= PhoneNumber.Length && PhoneNumber.Length <= 12)
                return true;
            else
                return false;
            //var regex = new Regex(AppConstant.PhoneRegex, RegexOptions.IgnoreCase);
            //return regex.IsMatch(PhoneNumber);

        }
        public static bool PasswordValidation(string Password)
        {
            if (string.IsNullOrEmpty(Password))
                return false;
            var regex = new Regex(AppConstant.PasswordRegex, RegexOptions.IgnoreCase);
            var isMatch = regex.IsMatch(Password);
            return isMatch;

        }
        public static bool EmptyString(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            else
                return true;
        }
    }
}
