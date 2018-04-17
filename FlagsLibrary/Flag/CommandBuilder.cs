using System;
using System.Collections.Generic;

namespace Flag
{
    /// <summary>
    /// To build command
    /// contains add flag option, set command definition
    /// </summary>
    public class CommandBuilder
    {
        internal List<FlagOption> CommandFlagOptions = new List<FlagOption>();
        private readonly ArgsParserBuilder argsParserBuilder;

        public CommandBuilder(ArgsParserBuilder argsParserBuilder)
        {
            this.argsParserBuilder = argsParserBuilder;
        }

        /// <summary>
        /// add flag options to one command
        /// </summary>
        /// <param name="fullName"></param>
        /// <param name="abbreviationName"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public CommandBuilder AddFlagOption(string fullName, char? abbreviationName, string description)
        {
            ValidateDuplicateFlagName(fullName, abbreviationName);
            CommandFlagOptions.Add(new FlagOption(fullName, abbreviationName, description));
            return this;
        }

        /// <summary>
        /// End the current command definition and set default command in args parser
        /// </summary>
        /// <returns></returns>
        public ArgsParserBuilder EndCommand()
        {
            argsParserBuilder.parser.defaultCommandBuilder = this;
            return argsParserBuilder;
        }

        void ValidateDuplicateFlagName(string fullName, char? abbreviationName)
        {
            if (CommandFlagOptions.Find(
                    f => (abbreviationName != null && f.AbbreviationName == abbreviationName) ||
                         (fullName != null && f.FullName == fullName)) != null)
            {
                throw new ArgumentException();
            }
        }

    }
}