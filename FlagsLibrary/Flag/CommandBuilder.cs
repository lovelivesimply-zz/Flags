using System;
using System.Collections.Generic;

namespace Flag
{
    public class CommandBuilder
    {
        internal List<FlagOption> CommandFlagOptions = new List<FlagOption>();
        private readonly ArgsParserBuilder argsParserBuilder;

        public CommandBuilder(ArgsParserBuilder argsParserBuilder)
        {
            this.argsParserBuilder = argsParserBuilder;
        }

        public CommandBuilder AddFlagOption(string fullName, char? abbreviationName, string description)
        {
            ValidateDuplicateFlagName(fullName, abbreviationName);
            CommandFlagOptions.Add(new FlagOption(fullName, abbreviationName, description));
            return this;
        }

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