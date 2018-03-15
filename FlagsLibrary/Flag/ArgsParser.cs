using System.IO;
using System.Reflection;

namespace Flag
{
    public class ArgsParser
    {
        public string FullName { get; set; }
        public string AbbreviationName { get; set; }
        public string Description { get; set; }

        public ArgsParser()
        {
        }

        public ArgsParser(string fullName, string abbreviationName, string description)
        {
            this.FullName = fullName;
            this.AbbreviationName = abbreviationName;
            this.Description = description;
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