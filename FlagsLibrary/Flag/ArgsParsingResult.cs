using System.Collections.Generic;

namespace Flag
{
    public class ArgsParsingResult
    {
        private readonly FlagOption flagOption; // will be an array later
        public ArgsParsingResult(FlagOption flagOption)
        {
            this.flagOption = flagOption;
        }

        public ArgsParsingResult(bool isSuccess, FlagOption flagOption)
        {
            IsSuccess = isSuccess;
            this.flagOption = flagOption;
        }

        public bool IsSuccess { get; set; }

        public bool GetFlagValue(string flag)
        {
            return $"--{flagOption.FullName}".Equals(flag) || $"-{flagOption.AbbreviationName}".Equals(flag);
        }

        public Error Error { get; set; }
    }

    public class Error
    {
        public ParsingErrorCode Code { get; set; }
        public string Trigger { get; set; }
    }

    public enum ParsingErrorCode
    {
        UndefinedOption,
        InvalidOptionName
    }
}