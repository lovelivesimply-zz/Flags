using System.Collections.Generic;

namespace Flag
{
    public class ArgsParsingResult
    {
        readonly HashSet<string> flagValues = new HashSet<string>();
        public ArgsParsingResult()
        {
        }

        public ArgsParsingResult(bool isSuccess, string fullName, string abbreviationName)
        {
            IsSuccess = isSuccess;
            flagValues.Add(fullName);
            flagValues.Add(abbreviationName);
        }

        public bool IsSuccess { get; set; }

        public bool GetFlagValue(string flag)
        {

            return flagValues.Contains(flag);
        }
    }
}