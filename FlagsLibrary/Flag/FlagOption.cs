using System;
using System.Text.RegularExpressions;

namespace Flag
{
    public class FlagOption
    {
        string fullNamePattern = @"^[a-zA-Z0-9_][a-zA-Z0-9_-]*$";

        public FlagOption()
        {
        }

        public FlagOption(string fullName, char? abbreviationName, string description)
        {
            ValidateFlagName(fullName, abbreviationName);
            FullName = fullName;
            AbbreviationName = abbreviationName;
            Description = description;
        }

        public string FullName { get; set; }
        public char? AbbreviationName { get; set; }
        private string Description { get; set; }

        void ValidateFlagName(string fullName, char? abbreviationName)
        {
            if (fullName == null && abbreviationName == null)
            {
                throw new ArgumentNullException();
            }

            if (fullName != null && !new Regex(fullNamePattern).IsMatch(fullName))
            {
                throw new ArgumentException();
            }

            if (abbreviationName != null && !Char.IsLetter((char)abbreviationName))
            {
                throw new ArgumentException();
            }
        }
    }
}