using System.Text.RegularExpressions;

namespace Flag
{
    public static class ParameterValidator
    {
        static string fullNamePattern = @"^[a-zA-Z0-9_][a-zA-Z0-9_-]*$";
        static string abbrNamePattern = @"^[a-zA-Z]$";

        /// <summary>
        /// accept string to validate the format is valid
        /// </summary>
        /// <param name="flag"> string be parsed</param>
        /// <returns>return true when parsing valid flag full name or abbreviation name</returns>
        public static bool ValidateFlagNameFormat(string flag)
        {
            return new Regex(fullNamePattern).IsMatch(flag.Substring(2)) && flag.StartsWith("--")
                   || new Regex(abbrNamePattern).IsMatch(flag.Substring(1)) && flag.StartsWith("-");

        }
    }
}