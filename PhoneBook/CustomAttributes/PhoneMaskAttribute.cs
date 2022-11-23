using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.RegularExpressions;
using NLog;

namespace PhoneBook.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class PhoneMaskAttribute : ValidationAttribute
    {
        int errMsgStat;
        private readonly Logger logger = LogManager.GetCurrentClassLogger(); // creates a logger using the class name

        public PhoneMaskAttribute()
        {
            errMsgStat = 0;
        }


        public override bool IsValid(object value)
        {
            var phoneNumber = (String)value;
            bool result = true;
            result = MatchesMask(phoneNumber);
            return result;
        }
        internal bool MatchesMask(string phoneNumber)
        {
            if (phoneNumber.Trim().Length == 0)
            {
                errMsgStat = 0;
                logger.Error("No Phone Number Provided!!");
                return false;
            }
            if (phoneNumber.Length >= 5)
            {
                var regex = @"(^(?<withoutBrackets>((?(\+)((\+){1}([\d]){0,3})|([\d]{0,3}))[ .-]{0,1}){0,1}(([0-9]{3})[ .-]{0,1}){0,1}(([0-9]{3}[ .-]{1}){1}(([0-9]{4})){1}))$)"
+@"|(^(?<withBrackets>((?(\+)((\+){1}(([1-9]{1})(\d){0,2}))|(([1-9]{1})(\d){0,2}))){0,1})(?((?<cp7>([ ]{0,1}\()))(\k<cp7>([1-9]{1})(\d){0,2}\)))([ ]{0,1}[\d]){3}-([-]{0,1}[\d]{4})$)"
+@"|(^(?<fiveDigits>((?((?<cp3>([\d]{5})))(\k<cp3>([.-]{1}[\d]{5}){0,1}))))$)|(^[\d]{3} [\d]{1} [\d]{3} [\d]{3} [\d]{4}$)"+
@"|(^((\(?\+45\)?)?)([\s.]{0,1}\d{2}[\s.]{0,1}\d{2}[\s.]{0,1}\d{2}[\s.]{0,1}\d{2})$)";


                if (Regex.IsMatch(phoneNumber, regex))
                {
                    return true;
                }
                else
                {
                    errMsgStat = 2;
                    logger.Error("Invalid Phone Number Provided-->" + "'" + phoneNumber.ToString() + "'");
                    return false;
                }
            }
            else
            {
                errMsgStat = 1;
                logger.Error("Invalid Phone Number Provided-->" + "'" + phoneNumber.ToString() + "'");
                return false;
            }

        }

        public override string FormatErrorMessage(string name)
        {
            var msg = ErrorMessageString;
            if (errMsgStat == 0)
            {
                msg = "Length Must be More than 0.";
            }
            if (errMsgStat == 1)
            {
                msg = "Not a Valid 5-Digit Extension!";
            }
            if (errMsgStat == 2)
            {
                msg = "The Number Provided does not match with the format prescribed!!!";
            }
            return String.Format(CultureInfo.CurrentCulture, msg);
        }

    }
}