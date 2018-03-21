using System;
using System.Text.RegularExpressions;

namespace Flag
{
    /// <summary>
    /// a builder to build ArgsParser
    /// </summary>
    public class ArgsParserBuilder
    {
        private readonly ArgsParser parser = new ArgsParser();
        string fullNamePattern = @"^[a-zA-Z0-9_][a-zA-Z0-9_-]*$";

        /// <summary>
        /// used to add flag option
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="abbreviationName"></param>
        /// <param name="description"></param>
        /// <returns>return the builder itself, so that we can add more flag option</returns>
        public ArgsParserBuilder AddFlagOption(string fullName, char? abbreviationName, string description)
        {

            ValidFlagName(fullName, abbreviationName);
            parser.flagOptions.Add(new FlagOption(fullName, abbreviationName, description));
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

        /// <returns>return a parser</returns>
        public ArgsParser Build()
        {
            return parser;
        }
    }
}