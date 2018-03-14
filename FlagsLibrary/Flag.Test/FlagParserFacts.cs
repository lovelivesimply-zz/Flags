using Xunit;

namespace Flag.Test
{
    public class FlagParserFacts
    {
        [Fact]
        void should_parse_success_and_get_flag_value_when_has_flag_full_name()
        {
            var fullName = "flag";
            var description = "the first flag";

            ArgsParser parser = new ArgsParserBuilder().AddFlagOption(fullName, null, description).Build();

            ArgsParsingResult result = parser.Parser(new[] { "--flag" });

            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("--flag"));
        }

        [Fact]
        void should_parse_success_and_can_get_flag_value_when_flag_has_abbreviation_name()
        {
            var abbrName = "f";
            var description = "the first flag";

            ArgsParser parser = new ArgsParserBuilder().AddFlagOption(null, abbrName, description).Build();

            ArgsParsingResult result = parser.Parser(new[] { "-f" });

            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("-f"));
        }

        [Fact]
        void should_parse_success_and_can_get_flag_value_when_flag_has_two_valid_names()
        {
            var fullName = "flag";
            var abbrName = "f";
            var description = "the first flag";

            ArgsParser parser = new ArgsParserBuilder().AddFlagOption(fullName, abbrName, description).Build();

            ArgsParsingResult result = parser.Parser(new[] { "-f" });
            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("--flag"));

            ArgsParsingResult result2 = parser.Parser(new[] { "--flag" });
            Assert.True(result2.IsSuccess);
            Assert.True(result2.GetFlagValue("-f"));
        }
    }
}