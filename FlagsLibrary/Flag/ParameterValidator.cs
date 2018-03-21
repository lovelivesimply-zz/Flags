using System;
using System.Text.RegularExpressions;

namespace Flag
{
    public class ParameterValidator
    {
        string fullNamePattern = @"^[a-zA-Z0-9_][a-zA-Z0-9_-]*$";
        string abbrNamePattern = @"^[a-zA-Z]$";

        /// <summary>
        /// accept string to validate the format is valid
        /// </summary>
        /// <param name="flag"> string be parsed</param>
        /// <returns>return true when parsing valid flag full name or abbreviation name</returns>
        public bool ValidateFlagNameFormat(string flag)
        {
            return new Regex(fullNamePattern).IsMatch(flag.Substring(2)) &&
                   flag.IndexOf("--", StringComparison.Ordinal) == 0
                   || new Regex(abbrNamePattern).IsMatch(flag.Substring(1)) &&
                   flag.IndexOf("-", StringComparison.Ordinal) == 0;
        
    }
}