using System;

namespace Flag
{
    /// <summary>
    /// a builder to build ArgsParser
    /// </summary>
    public class ArgsParserBuilder
    {

        internal readonly ArgsParser parser = new ArgsParser();

        /// <summary>
        /// used to add flag option
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="abbreviationName"></param>
        /// <param name="description"></param>
        /// <returns>return the builder itself, so that we can add more flag option</returns>

        /// <returns>return a parser</returns>
        public ArgsParser Build()
        {
            return parser;
        }

        public CommandBuilder BeginDefaultCommand()
        {
            return new CommandBuilder(this);
        }
    }
}