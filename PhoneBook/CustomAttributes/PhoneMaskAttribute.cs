using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Numerics;



namespace PhoneBook.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class PhoneMaskAttribute : ValidationAttribute
    {
        // Internal field to hold the mask value.
        //readonly string _mask;
        int errMsgStat;

        public PhoneMaskAttribute()
        {
            errMsgStat = 0;
        }


        public override bool IsValid(object value)
        {
            var phoneNumber = (String)value;
            bool result = true;
            //if (this.Mask != null)
            //{
            result = MatchesMask(phoneNumber);
            //}
            return result;
        }

        // Checks if the entered phone number matches the mask.
        internal bool MatchesMask(string phoneNumber)
        {
            if (phoneNumber.Trim().Length == 0)
            {
                // Length mismatch.
                errMsgStat = 0;
                return false;
            }
            int ph;
            /*
             * Some organizations use 5-digit extensions only 
             * for dialing from one internal phone to another.[Validation Below]
             */
            if (phoneNumber.Length == 5 && Int32.TryParse(phoneNumber, out ph) == true)
            {
                return true;
            }
            else
            {
                errMsgStat = 1;
                return false;
            }
            //-----------------------------------------------------------------------------

            //for (int i = 0; i < mask.Length; i++)
            //{
            //    if (mask[i] == 'd' && char.IsDigit(phoneNumber[i]) == false)
            //    {
            //        // Digit expected at this position.
            //        return false;
            //    }
            //    if (mask[i] == '-' && phoneNumber[i] != '-')
            //    {
            //        // Spacing character expected at this position.
            //        return false;
            //    }
            //}
            return true;
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
            return String.Format(CultureInfo.CurrentCulture, msg);
        }

    }
}