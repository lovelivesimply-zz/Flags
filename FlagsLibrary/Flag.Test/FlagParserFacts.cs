using System;
using System.IO;
using Xunit;

namespace Flag.Test
{
    public class FlagParserFacts
    {
        [Fact]
        public void should_parse_success_and_get_flag_value_when_has_flag_full_name()
        {
            var fullName = "flag";
            var description = "the first flag";

            ArgsParser parser = new ArgsParserBuilder().AddFlagOption(fullName, null, description).Build();

            ArgsParsingResult result = parser.Parser(new[] { "--flag" });

            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("--flag"));
            Assert.Null(result.Error);
        }

        [Fact]
        public void should_parse_success_and_can_get_flag_value_when_flag_has_abbreviation_name()
        {
            var abbrName = "f";
            var description = "the first flag";

            ArgsParser parser = new ArgsParserBuilder().AddFlagOption(null, abbrName, description).Build();

            ArgsParsingResult result = parser.Parser(new[] { "-f" });

            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("-f"));
            Assert.Null(result.Error);
        }

        [Fact]
        public void should_parse_success_and_can_get_flag_value_when_flag_has_two_valid_names()
        {
            var fullName = "flag";
            var abbrName = "f";
            var description = "the first flag";

            ArgsParser parser = new ArgsParserBuilder().AddFlagOption(fullName, abbrName, description).Build();

            ArgsParsingResult result = parser.Parser(new[] { "-f" });
            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("--flag"));
            Assert.Null(result.Error);

            ArgsParsingResult result2 = parser.Parser(new[] { "--flag" });
            Assert.True(result2.IsSuccess);
            Assert.True(result2.GetFlagValue("-f"));
            Assert.Null(result2.Error);
        }

        [Fact]
        public void should_throw_exception_when_add_flag_without_full_name_and_abbr_name()
        {
            Assert.Throws<ArgumentNullException>(() => new ArgsParserBuilder().AddFlagOption(null, null, "description"));
            Assert.Throws<ArgumentException>(() => new ArgsParserBuilder().AddFlagOption("", null, "description"));
            Assert.Throws<ArgumentException>(() => new ArgsParserBuilder().AddFlagOption(null, "", "description"));
            Assert.Throws<ArgumentException>(() => new ArgsParserBuilder().AddFlagOption("flag", "ff", "description"));
            Assert.Throws<ArgumentException>(() => new ArgsParserBuilder().AddFlagOption("-flag", "f", "description"));
        }

        [Fact]
        public void should_throw_exception_when_parse_parameters_is_empty_or_multiple()
        {
            var fullName = "flag";
            var abbrName = "f";
            var description = "the first flag";

            ArgsParser parser = new ArgsParserBuilder().AddFlagOption(fullName, abbrName, description).Build();

            Assert.Throws<InvalidDataException>(() => parser.Parser(new string[] {}));
            Assert.Throws<InvalidDataException>(() => parser.Parser(new[] { "-f", "-flag" }));
        }

        [Fact]
        public void should_return_false_when_get_flag_value_with_wrong_parameters()
        {
            var fullName = "flag";
            var abbrName = "f";
            var description = "the first flag";

            ArgsParser parser = new ArgsParserBuilder().AddFlagOption(fullName, abbrName, description).Build();

            ArgsParsingResult result = parser.Parser(new[] { "-f" });

            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("--flag"));
            Assert.False(result.GetFlagValue("--f"));
            Assert.False(result.GetFlagValue("-flag"));
            Assert.False(result.GetFlagValue("-v"));
        }

        [Fact]
        public void should_throw_exception_and_get_message_when_the_parse_parameter_is_invalid()
        {
            var fullName = "flag";
            var abbrName = "f";
            var description = "the first flag";

            ArgsParser parser = new ArgsParserBuilder().AddFlagOption(fullName, abbrName, description).Build();

            Assert.Throws<InvalidDataException>(() => parser.Parser(new []{"-v"}));
        }
    }
}