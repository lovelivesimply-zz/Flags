using System;
using System.Text.RegularExpressions;

namespace Flag
{
    public class ArgsParserBuilder
    {
        private readonly ArgsParser parser = new ArgsParser();
        string fullNamePattern = @"^[a-zA-Z0-9_][a-zA-Z0-9_-]*$";
        string abbrNamePattern = @"^[a-zA-Z]$";
        public ArgsParserBuilder AddFlagOption(string fullName, char? abbreviationName, string description)
        {

            ValidFlagName(fullName, abbreviationName);
            parser.FlagOption = new FlagOption(fullName, abbreviationName, description);
            return this;
        }

        void ValidFlagName(string fullName, char? abbreviationName)
        {
            if (fullName == null && abbreviationName == null)
            {
                throw new ArgumentNullException();
            }

            if (fullName != null && !new Regex(fullNamePattern).IsMatch(fullName))
            {
                throw new ArgumentException();
            }

            if (abbreviationName != null && !Char.IsLetter((char) abbreviationName))
            {
                throw new ArgumentException();
            }
        }

        public ArgsParser Build()
        {
            return parser;
        }
    }
}