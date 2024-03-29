﻿using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.RegularExpressions;
using NLog;

namespace PhoneBook.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    sealed public class NameValidationAttribute : ValidationAttribute
    {
        int errMsgStat;
        private readonly Logger logger = LogManager.GetCurrentClassLogger(); // creates a logger using the class name

        public NameValidationAttribute()
        {
            errMsgStat = 0;
        }


        public override bool IsValid(object value)
        {
            var name = (String)value;
            bool result = true;
            result = MatchesMask(name);
            return result;
        }

        internal bool MatchesMask(string name)
        {
            if (name.Trim().Length < 2)
            {
                // Name should be minimum of 2 Characters.
                errMsgStat = 0;
                logger.Error("No Name Provided!!");
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
                    logger.Error("Invalid Name Provided-->" + "'" + name.ToString() + "'");
                    return false;
                }
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