namespace Flag
{
    public class FlagOption
    {
        public FlagOption()
        {
        }

        public FlagOption(string fullName, string abbreviationName, string description)
        {
            FullName = fullName;
            AbbreviationName = abbreviationName;
            Description = description;
        }

        public string FullName { get; set; }
        public string AbbreviationName { get; set; }
        private string Description { get; set; }
    }
}