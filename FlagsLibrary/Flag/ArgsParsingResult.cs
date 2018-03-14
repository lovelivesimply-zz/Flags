namespace Flag
{
    public class ArgsParsingResult
    {
        public bool IsSuccess { get; set; }

        public bool GetFlagValue(string flag)
        {
            if ($"--{ArgsParserBuilder.parser.FullName}" == flag || $"-{ArgsParserBuilder.parser.AbbreviationName}" == flag)
            {
                return true;
            }
            return false;
        }
    }
}