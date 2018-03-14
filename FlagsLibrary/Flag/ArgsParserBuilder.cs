using System.IO;
using System.Text.RegularExpressions;

namespace Flag
{
    public class ArgsParserBuilder
    {
        public ArgsParser parser = new ArgsParser();
        string fullNamePattern = @"^[a-zA-Z0-9_][a-zA-Z0-9_-]*$";
        string abbrNamePattern = @"^[a-zA-Z]$";
        public ArgsParserBuilder AddFlagOption(string fullName, string abbreviationName, string description)
        {
            if (fullName == null && abbreviationName == null)
            {
                throw new InvalidDataException();
            }

            if (fullName != null && !new Regex(fullNamePattern).IsMatch(fullName))
            {
                throw new InvalidDataException();
            }

            if (abbreviationName != null && !new Regex(abbrNamePattern).IsMatch(abbreviationName))
            {
                throw new InvalidDataException();
            }

            parser.FullName = fullName;
            parser.AbbreviationName = abbreviationName;
            parser.Description = description;
            return this;
        }

        public ArgsParser Build()
        {
            return parser;
        }
    }
}