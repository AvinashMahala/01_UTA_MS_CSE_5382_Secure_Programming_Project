using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.RegularExpressions;

namespace PhoneBook.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class NameValidationAttribute : ValidationAttribute
    {
        // Internal field to hold the mask value.
        //readonly string _mask;
        int errMsgStat;

        public NameValidationAttribute()
        {
            errMsgStat = 0;
        }


        public override bool IsValid(object value)
        {
            var name = (String)value;
            bool result = true;
            //if (this.Mask != null)
            //{
            result = MatchesMask(name);
            //}
            return result;
        }

        // Checks if the entered phone number matches the mask.
        internal bool MatchesMask(string name)
        {
            if (name.Trim().Length < 2)
            {
                // Name should be minimum of 2 Characters.
                errMsgStat = 0;
                return false;
            }
            else
            {
                var regex = @"(^([A-Z]')*([a-zA-Z]{2,}){1}, ([a-zA-Z]+){1}( ([a-zA-Z]+.){1})*$)|(^[a-zA-Z]*[.]{0,1}[ ]{0,1}[a-zA-Z]{0,1}[']{0,1}[a-zA-Z]*[- ]{0,1}[a-zA-Z]*$)";


                if (Regex.IsMatch(name, regex))
                {
                    return true;
                }
                else
                {
                    errMsgStat = 1;
                    return false;
                }
                return true;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            var msg = ErrorMessageString;
            if (errMsgStat == 0)
            {
                msg = "Name should be minimum of 2 Characters.";
            }
            if (errMsgStat == 1)
            {
                msg = "Invalid Name!";
            }
            return String.Format(CultureInfo.CurrentCulture, msg);
        }

    }
}