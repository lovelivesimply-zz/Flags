using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Flag
{
    public class ArgsParsingResult
    {
        string fullNamePattern = @"^[a-zA-Z0-9_][a-zA-Z0-9_-]*$";
        string abbrNamePattern = @"^[a-zA-Z]$";

        internal List<FlagOption> FlagOptions = new List<FlagOption>();
        /// <summary>
        /// bool, respresent the flag parsing status
        /// </summary>
        public bool IsSuccess { get; set; }
        readonly ParameterValidator parameterValidator = new ParameterValidator();

        /// <summary>
        /// currently is bool, a result status to get flag fullName or abbreviationName
        /// </summary>
        /// <param name="flag">a string which is the flag fullName or abbreviation name</param>
        /// <returns>return true if can get the flag fullName or abbreviationName</returns>
        /// <returns>return false if can get the flag fullName or abbreviationName</returns>
        public bool GetFlagValue(string flag)
        {
            if (flag == null) throw new ArgumentException();

            var isFlagNameValid = parameterValidator.ValidateFlagNameFormat(flag);

            if (!isFlagNameValid) throw new ArgumentException();

            if (!IsSuccess) throw new InvalidOperationException();

            var flagOption = FlagOptions.Find(f => $"-{f.AbbreviationName}" == flag || $"--{f.FullName}" == flag);
            if (flagOption == null) throw new ArgumentException();
            return true;
        }

        /// <summary>
        /// parsing result error, contains an error code and triggered value
        /// if parsing successfully, error is null
        /// </summary>
        public Error Error { get; set; }
    }
    
    public class Error
    {
        public Error(ParsingErrorCode parsingErrorCode, string trigger)
        {
            Code = parsingErrorCode;
            Trigger = trigger;
        }

        public ParsingErrorCode Code { get; }
        public string Trigger { get; }
    }

    public enum ParsingErrorCode
    {
        UndefinedOption,
        InvalidOptionName,
        FreeValueNotSupported,
        DuplicateFlagsInArgs
    }
}