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
        // Internal field to hold the mask value.
        //readonly string _mask;
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
                logger.Error("No Phone Number Provided!!");
                return false;
            }
            int ph;
            /*
             * Some organizations use 5-digit extensions only 
             * for dialing from one internal phone to another.[Validation Below]
             */
            //&& Int32.TryParse(phoneNumber, out ph) ==true
            if (phoneNumber.Length >= 5)
            {
                var regex = @"^(?<one>[0-1]{0,3})?(\.|\-|\s)?(?<areaCode>[0-9]\d{0,2})?(\.|\-|\s)?(?<number>[1-9][\d]{0,2})?(\.|\-|\s)?(?<SubscriberNumber>\d{0,4})?$";


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
                return true;
            }
            else
            {
                errMsgStat = 1;
                logger.Error("Invalid Phone Number Provided-->" + "'" + phoneNumber.ToString() + "'");
                return false;
            }
            /*
             The country code may or may not be preceded by a + which indicates that an international
                dialing prefix, such as 00 or 011, must be included when dialing. If not using the plus, the
                dialing prefix itself may be included.
             */
            //-----------------------------------------------------------------------------

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
                msg = "Rest All!";
            }
            return String.Format(CultureInfo.CurrentCulture, msg);
        }

    }
}