using System;
using System.Collections.Generic;
using System.Linq;

namespace Flag
{
    /// <summary>
    /// provide parse function and build from ArgsParserBuilder
    /// </summary>
    public class ArgsParser
    {
        internal List<FlagOption> flagOptions = new List<FlagOption>();

        /// <summary>
        /// accept a to be parsed string array and return a parsing result
        /// </summary>
        /// <param name="flags"> string array to be parsed</param>
        /// <returns>return a parsing result with IsSucccess is true when parsing successfully</returns>
        /// <returns>return a parsing result with IsSucccess is false and error code and trigger when parsing failed</returns>
        public ArgsParsingResult Parser(string[] flags)
        {
            ValidateParamete(flags);
            string duplicatedFlag;
            if (!ValidateDuplicateFlagName(flags, out duplicatedFlag))
            {
                return new ArgsParsingResult(false, null, new Error(ParsingErrorCode.DuplicateFlagsInArgs, duplicatedFlag));
            }
            var parsingResultOptions = new List<FlagOption>();
            foreach (var flag in flags)
            {
                if (!ParameterValidator.ValidateFlagNameFormat(flag))
                {
                    return new ArgsParsingResult(false, null, new Error(ParsingErrorCode.FreeValueNotSupported, flag));
                }
                var flagOption = flagOptions.Find(f => $"-{f.AbbreviationName}" == flag || $"--{f.FullName}" == flag);
                if (flagOption == null)
                {
                    return new ArgsParsingResult(false, null, new Error(ParsingErrorCode.FreeValueNotSupported, flag));
                }

                if (parsingResultOptions.Contains(flagOption))
                {
                    return new ArgsParsingResult(false, null, new Error(ParsingErrorCode.DuplicateFlagsInArgs, flag));
                }
                parsingResultOptions.Add(flagOption);
            }

            return new ArgsParsingResult(true, parsingResultOptions, null);
        }

        void ValidateParamete(string[] flags)
        {
            if (flags == null || flags.Any(f => f == null))
            {
                throw new ArgumentException();
            }
        }

        bool ValidateDuplicateFlagName(string[] flags, out string duplicatedFlag)
        {
            HashSet<string> toBeParsed = new HashSet<string>();
            foreach (var flag in flags)
            {
                if (toBeParsed.Contains(flag))
                {
                    duplicatedFlag = flag;
                    return false;
                }
                toBeParsed.Add(flag);
            }
            duplicatedFlag = null;
            return true;
        }
    }
}