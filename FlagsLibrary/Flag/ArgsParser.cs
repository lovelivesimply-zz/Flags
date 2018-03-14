using System;

namespace Flag
{
    public class ArgsParser
    {
        public string FullName { get; set; }
        public string AbbreviationName { get; set; }
        public string Description { get; set; }
        private ArgsParsingResult parsingResult = new ArgsParsingResult();
        public ArgsParser(string fullName, string abbreviationName, string description)
        {
            this.FullName = fullName;
            this.AbbreviationName = abbreviationName;
            this.Description = description;
        }

        public ArgsParser()
        {
        }

        public ArgsParsingResult Parser(string[] flags)
        {
            if (flags.Length != 1)
            {
                throw new InvalidOperationException();
            }
            if (flags[0] == $"--{FullName}" || flags[0] == $"-{AbbreviationName}")
            {
                parsingResult.IsSuccess = true;
            }
            return parsingResult;
        }
    }
}