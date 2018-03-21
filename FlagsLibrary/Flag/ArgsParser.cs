using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Flag
{
    /// <summary>
    /// provide parse function and build from ArgsParserBuilder
    /// </summary>
    public class ArgsParser
    {
        internal List<FlagOption> flagOptions = new List<FlagOption>();
        string fullNamePattern = @"^[a-zA-Z0-9_][a-zA-Z0-9_-]*$";
        string abbrNamePattern = @"^[a-zA-Z]$";
        bool isFlagNameValid;

        /// <summary>
        /// accept a to be parsed string array and return a parsing result
        /// </summary>
        /// <param name="flags"> string array to be parsed</param>
        /// <returns>return a parsing result with IsSucccess is true when parsing successfully</returns>
        /// <returns>return a parsing result with IsSucccess is false and error code and trigger when parsing failed</returns>
        public ArgsParsingResult Parser(string[] flags)
        {
            var argsParsingResult = new ArgsParsingResult();

            ValidateParamete(flags);

            foreach (var flag in flags)
            {
                isFlagNameValid = new Regex(fullNamePattern).IsMatch(flag.Substring(2)) && flag.IndexOf("--", StringComparison.Ordinal) == 0
                                    ||new Regex(abbrNamePattern).IsMatch(flag.Substring(1)) && flag.IndexOf("-", StringComparison.Ordinal) == 0;

                if (!isFlagNameValid)
                {
                    argsParsingResult.IsSuccess = false;
                    argsParsingResult.FlagOptions = null;
                    argsParsingResult.Error = new Error(ParsingErrorCode.FreeValueNotSupported, flag);
                    return argsParsingResult;
                }

                var flagOption = flagOptions.Find(f => $"-{f.AbbreviationName}" == flag || $"--{f.FullName}" == flag);
                if (flagOption == null)
                {
                    argsParsingResult.IsSuccess = false;
                    argsParsingResult.FlagOptions = null;
                    argsParsingResult.Error = new Error(ParsingErrorCode.FreeValueNotSupported, flag);
                    return argsParsingResult;
                }

                if (argsParsingResult.FlagOptions.Contains(flagOption))
                {
                    argsParsingResult.IsSuccess = false;
                    argsParsingResult.Error = new Error(ParsingErrorCode.DuplicateFlagsInArgs, flag);
                    return argsParsingResult;
                }
                argsParsingResult.IsSuccess = true;
                argsParsingResult.FlagOptions.Add(flagOption);
            }

            return argsParsingResult;
        }

        void ValidateParamete(string[] flags)
        {
            if (flags == null)
            {
                throw new ArgumentException();
            }

            foreach (var flag in flags)
            {
                if (flag == null)
                {
                    throw new ArgumentException();
                }
            }
        }
    }
}