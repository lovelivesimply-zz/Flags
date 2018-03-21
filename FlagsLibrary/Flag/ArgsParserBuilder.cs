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

        /// <summary>
        /// used to add flag option
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="abbreviationName"></param>
        /// <param name="description"></param>
        /// <returns>return the builder itself, so that we can add more flag option</returns>
        public ArgsParserBuilder AddFlagOption(string fullName, char? abbreviationName, string description)
        {
            if (abbreviationName!=null && parser.flagOptions.Find(f => f.AbbreviationName == abbreviationName) != null)
            {
                throw new ArgumentException();
            }
            parser.flagOptions.Add(new FlagOption(fullName, abbreviationName, description));
            return this;
        }

        

        /// <returns>return a parser</returns>
        public ArgsParser Build()
        {
            return parser;
        }
    }
}