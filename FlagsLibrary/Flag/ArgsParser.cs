using System.IO;

namespace Flag
{
    public class ArgsParser
    {
        internal FlagOption flagOption; // currently is single, will be an array later.

        public ArgsParsingResult Parser(string[] flags)
        {
            if (flags.Length != 1)
            {
                throw new InvalidDataException();
            }
            if (flags[0] == $"--{flagOption.FullName}" || flags[0] == $"-{flagOption.AbbreviationName}")
            {
                return new ArgsParsingResult(true, flagOption);
            }
            throw new InvalidDataException($"can not parse flag {flags[0]}");
        }
    }
}