using System;
using Xunit;

namespace Flag.Test
{
    public class FlagParserFacts
    {
        [Fact]
        void should_success_and_get_flag_value_when_has_flag_full_name()
        {
            var fullName = "flag";
            var description = "the first flag";

            ArgsParser parser = new ArgsParserBuilder().AddFlagOption(fullName, null, description).Build();

            ArgsParsingResult result = parser.Parser(new[] { "--flag" });

            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("--flag"));
        }

        [Fact]
        void should_success_and_get_flag_value_when_add_flag_with_description_is_null()
        {
            var fullName = "flag";
            var abbreviation = "f";
            var parser = new ArgsParserBuilder().AddFlagOption(fullName, abbreviation, null).Build();

            ArgsParsingResult result = parser.Parser(new[] { "--flag" });

            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("-f"));
        }

        [Fact]
        void should_success_and_get_flag_value_when_add_flag_with_full_name_is_null()
        {
            var abbreviation = "f";
            var description = "the first flag";

            var parser = new ArgsParserBuilder().AddFlagOption(null, abbreviation, description).Build();

            ArgsParsingResult result = parser.Parser(new[] { "-f" });

            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("-f"));
        }

        [Fact]
        void should_success_and_get_flag_value_when_add_flag_with_legal_parameter()
        {
            var fullName = "flag";
            var abbreviation = "f";
            var description = "the first flag";
            var parser = new ArgsParserBuilder().AddFlagOption(fullName, abbreviation, description).Build();

            ArgsParsingResult result = parser.Parser(new[] { "--flag" });

            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("-f"));
        }

        [Fact]
        void should_throw_exception_when_add_flag_with_abbreviation_and_full_name_are_all_null()
        {
            var description = "the first flag";

            Assert.Throws<Exception>(() => new ArgsParserBuilder().AddFlagOption(null, null, description).Build());
        }
    }
}