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
        }

        [Fact]
        void should_success_and_get_flag_value_when_add_flag_with_description_is_null()
        {
            var fullName = "flag";
            var abbreviation = "f";
            var parser = new ArgsParserBuilder().AddFlagOption(fullName, abbreviation, null).Build();

            ArgsParsingResult result = parser.Parser(new[] {"--flag"});
            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("-f"));
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

            Assert.Throws<InvalidDataException>(() => new ArgsParserBuilder().AddFlagOption(null, null, description).Build());
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

            ArgsParsingResult result2 = parser.Parser(new[] { "--flag" });
            Assert.True(result2.IsSuccess);
            Assert.True(result2.GetFlagValue("-f"));
        }

        [Fact]
        public void should_throw_exception_when_add_flag_without_full_name_and_abbr_name()
        {
            Assert.Throws<InvalidDataException>(() => new ArgsParserBuilder().AddFlagOption(null, null, "description"));
            Assert.Throws<InvalidDataException>(() => new ArgsParserBuilder().AddFlagOption("", null, "description"));
            Assert.Throws<InvalidDataException>(() => new ArgsParserBuilder().AddFlagOption(null, "", "description"));
            Assert.Throws<InvalidDataException>(() => new ArgsParserBuilder().AddFlagOption("flag", "ff", "description"));
            Assert.Throws<InvalidDataException>(() => new ArgsParserBuilder().AddFlagOption("-flag", "f", "description"));
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