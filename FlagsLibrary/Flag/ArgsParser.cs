using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Flag
{
    /// <summary>
    /// provide parse function and build from ArgsParserBuilder
    /// </summary>
    public class ArgsParser
    {
        internal FlagOption FlagOption = new FlagOption(); // currently is single, will be an array later.
        string fullNamePattern = @"^[a-zA-Z0-9_][a-zA-Z0-9_-]*$";
        string abbrNamePattern = @"^[a-zA-Z]$";

        /// <summary>
        /// accept a to be parsed string array and return a parsing result
        /// </summary>
        /// <param name="flags"> string array to be parsed</param>
        /// <returns>return a parsing result with IsSucccess is true when parsing successfully</returns>
        /// <returns>return a parsing result with IsSucccess is false and error code and trigger when parsing failed</returns>
        public ArgsParsingResult Parser(string[] flags)
        {
            var argsParsingResult = new ArgsParsingResult();

            foreach (var flag in flags)
            {
                if (!((new Regex(fullNamePattern).IsMatch(flag.Substring(2)) && flag.IndexOf("--", StringComparison.Ordinal) == 0)
                    || (new Regex(abbrNamePattern).IsMatch(flag.Substring(1)) && flag.IndexOf("-", StringComparison.Ordinal) == 0)))
                {
                    argsParsingResult.IsSuccess = false;
                    argsParsingResult.FlagOption = null;
                    argsParsingResult.Error = new Error(ParsingErrorCode.InvalidOptionName, flag);
                    return argsParsingResult;
                }

                if (flag != $"--{FlagOption.FullName}" && flag != $"-{FlagOption.AbbreviationName}")
                {
                    argsParsingResult.IsSuccess = false;
                    argsParsingResult.FlagOption = null;
                    argsParsingResult.Error = new Error(ParsingErrorCode.UndefinedOption, flag);
                    return argsParsingResult;
                }
                if (flag == $"--{FlagOption.FullName}" || flag == $"-{FlagOption.AbbreviationName}")
                {
                    argsParsingResult.IsSuccess = true;
                    argsParsingResult.FlagOption = FlagOption;
                }
            }

            return argsParsingResult;
        }
    }
}