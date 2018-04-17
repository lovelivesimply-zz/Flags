using System;

namespace Flag
{
    /// <summary>
    /// a builder to build ArgsParser
    /// </summary>
    public class ArgsParserBuilder
    {

        internal readonly ArgsParser parser = new ArgsParser();
        internal bool hasDafaultCommand = false;
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


        /// <summary>
        /// begin to build command
        /// </summary>
        /// <returns>return command builder to add flag option and build command</returns>
        public CommandBuilder BeginDefaultCommand()
        {
            if (hasDafaultCommand) throw new InvalidOperationException();
            hasDafaultCommand = true;
            return new CommandBuilder(this);
        }
    }
}