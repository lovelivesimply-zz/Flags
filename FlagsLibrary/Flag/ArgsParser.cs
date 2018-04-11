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
        internal CommandBuilder defaultCommandBuilder;  // save all defined commands and will be a list later

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
                if (!ParameterValidator.ValidateFlagNameWhenParse(flag))
                {
                    return new ArgsParsingResult(false, null, new Error(ParsingErrorCode.FreeValueNotSupported, flag));
                }
                ArgsParsingResult invalidParserResult;
                if (flag.StartsWith("--"))
                {
                    var flagOption = defaultCommandBuilder.CommandFlagOptions.Find(f => $"--{f.FullName}" == flag);
                    if (CheckAndSaveParseFlag(flagOption, flag, parsingResultOptions, out invalidParserResult))
                        return invalidParserResult;
                }
                else if (flag.StartsWith("-"))
                {
                    var flagAbberviations = flag.Substring(1);
                    foreach (char flagAbberviation in flagAbberviations)
                    {
                        var flagOption = defaultCommandBuilder.CommandFlagOptions.Find(f => f.AbbreviationName.HasValue && char.ToLower(f.AbbreviationName.Value) == char.ToLower(flagAbberviation));
                        if (CheckAndSaveParseFlag(flagOption, flag, parsingResultOptions, out invalidParserResult))
                            return invalidParserResult;
                    }
                }
            }

            return new ArgsParsingResult(true, parsingResultOptions, null);
        }

        private bool CheckAndSaveParseFlag(FlagOption flagOption, string flag, List<FlagOption> parsingResultOptions, out ArgsParsingResult parser)
        {
            if (flagOption == null)
            {
                parser = new ArgsParsingResult(false, null, new Error(ParsingErrorCode.FreeValueNotSupported, flag));
                return true;
            }

            if (parsingResultOptions.Contains(flagOption))
            {
                 parser = new ArgsParsingResult(false, null, new Error(ParsingErrorCode.DuplicateFlagsInArgs, flag));
                 return true;
            }
            parser = new ArgsParsingResult();
            parsingResultOptions.Add(flagOption);
            return false;
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