using System;

namespace Flag
{
    public class ArgsParserBuilder
    {
        public static ArgsParser parser = new ArgsParser();
        public ArgsParserBuilder AddFlagOption(string fullName, string abbreviation, string description)
        {
            if (fullName == null && abbreviation == null)
            {
                throw new Exception("full name and abbreviation cannot be null at same time");
            }
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