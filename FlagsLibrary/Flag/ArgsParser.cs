using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Flag
{
    public class ArgsParser
    {
        internal FlagOption FlagOption = new FlagOption(); // currently is single, will be an array later.
        string fullNamePattern = @"^[a-zA-Z0-9_][a-zA-Z0-9_-]*$";
        string abbrNamePattern = @"^[a-zA-Z]$";

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