using System;
using System.Text.RegularExpressions;

namespace Flag
{
    public class ArgsParserBuilder
    {
        private readonly ArgsParser parser = new ArgsParser();
        string fullNamePattern = @"^[a-zA-Z0-9_][a-zA-Z0-9_-]*$";
        string abbrNamePattern = @"^[a-zA-Z]$";
        public ArgsParserBuilder AddFlagOption(string fullName, string abbreviationName, string description)
        {

            ValidFlagName(fullName, abbreviationName);
            parser.FlagOption = new FlagOption(fullName, abbreviationName, description);
            return this;
        }

        void ValidFlagName(string fullName, string abbreviationName)
        {
            if (fullName == null && abbreviationName == null)
            {
                throw new ArgumentNullException();
            }

            if (fullName != null && !new Regex(fullNamePattern).IsMatch(fullName))
            {
                throw new ArgumentException();
            }

            if (abbreviationName != null && !new Regex(abbrNamePattern).IsMatch(abbreviationName))
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