using System.IO;
using System.Text.RegularExpressions;

namespace Flag
{
    public class ArgsParserBuilder
    {
        private ArgsParser parser;
        string fullNamePattern = @"^[a-zA-Z0-9_][a-zA-Z0-9_-]*$";
        string abbrNamePattern = @"^[a-zA-Z]$";
        public ArgsParserBuilder AddFlagOption(string fullName, string abbreviationName, string description)
        {
            ValidFlagName(fullName, abbreviationName);
            parser = new ArgsParser(fullName, abbreviationName, description);
            return this;
        }

        void ValidFlagName(string fullName, string abbreviationName)
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
        }

        public ArgsParser Build()
        {
            return parser;
        }
    }
}