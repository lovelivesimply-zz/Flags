using System.IO;

namespace Flag
{
    public class ArgsParser
    {
        private string FullName { get; set; }
        private string AbbreviationName { get; set; }
        private string Description { get; set; }

        public ArgsParser()
        {
        }

        public ArgsParser(string fullName, string abbreviationName, string description)
        {
            FullName = fullName;
            AbbreviationName = abbreviationName;
            Description = description;
        }

        public ArgsParsingResult Parser(string[] flags)
        {
            if (flags.Length != 1)
            {
                throw new InvalidDataException();
            }
            if (flags[0] == $"--{FullName}" || flags[0] == $"-{AbbreviationName}")
            {
                return new ArgsParsingResult(true, $"--{FullName}", $"-{AbbreviationName}");
            }
            throw new InvalidDataException($"can not parse flag {flags[0]}");
        }
    }
}