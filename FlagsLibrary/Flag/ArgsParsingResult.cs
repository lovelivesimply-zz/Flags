using System;
using System.Collections.Generic;

namespace Flag
{
    public class ArgsParsingResult
    {
        internal List<FlagOption> FlagOptions = new List<FlagOption>();

        public ArgsParsingResult(bool isSuccess, List<FlagOption> flagOptions, Error error)
        {
            IsSuccess = isSuccess;
            FlagOptions = flagOptions;
            Error = error;
        }

        public ArgsParsingResult()
        {
        }

        /// <summary>
        /// bool, respresent the flag parsing status
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// currently is bool, a result status to get flag fullName or abbreviationName
        /// </summary>
        /// <param name="flag">a string which is the flag fullName or abbreviation name</param>
        /// <returns>return true if can get the flag fullName or abbreviationName</returns>
        /// <returns>return false if can get the flag fullName or abbreviationName</returns>
        public bool GetFlagValue(string flag)
        {
            if (flag == null || !ParameterValidator.ValidateFlagNameWhenGetValue(flag)) throw new ArgumentException();
            if (!IsSuccess) throw new InvalidOperationException();
            if (FlagOptions.Find(f => $"-{f.AbbreviationName}" == flag || $"--{f.FullName}" == flag) == null)
                throw new ArgumentException();
            return true;
        }

        /// <summary>
        /// parsing result error, contains an error code and triggered value
        /// if parsing successfully, error is null
        /// </summary>
        public Error Error { get; }
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
        FreeValueNotSupported,
        DuplicateFlagsInArgs
    }
}