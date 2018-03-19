namespace Flag
{
    public class ArgsParsingResult
    {
        internal FlagOption FlagOption; // will be an array later

        public bool IsSuccess { get; set; }

        public bool GetFlagValue(string flag)
        {
            if (FlagOption == null) return false;
            return $"--{FlagOption.FullName}".Equals(flag) || $"-{FlagOption.AbbreviationName}".Equals(flag);
        }

        public Error Error { get; set; }
    }

    public class Error
    {
        public Error(ParsingErrorCode parsingErrorCode, string trigger)
        {
            Code = parsingErrorCode;
            Trigger = trigger;
        }

        public ParsingErrorCode Code { get; set; }
        public string Trigger { get; set; }
    }

    public enum ParsingErrorCode
    {
        UndefinedOption,
        InvalidOptionName
    }
}