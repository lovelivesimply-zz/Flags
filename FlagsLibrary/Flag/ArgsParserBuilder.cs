namespace Flag
{
    public class ArgsParserBuilder
    {
        public static ArgsParser parser = new ArgsParser();
        public ArgsParserBuilder AddFlagOption(string fullName, string abbreviation, string description)
        {
            parser.FullName = fullName;
            parser.AbbreviationName = abbreviation;
            parser.Description = description;
            return this;
        }

        public ArgsParser Build()
        {
            return parser;
        }
    }
}