using System;
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
            var abbrName = 'f';
            var description = "the first flag";

            ArgsParser parser = new ArgsParserBuilder().AddFlagOption(null, abbrName, description).Build();

            ArgsParsingResult result = parser.Parser(new[] { "-f" });
            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("-f"));
        }

        [Fact]
        void should_success_and_get_flag_value_when_add_flag_with_description_is_null()
        {
            var fullName = "flag";
            var abbreviation = 'f';
            var parser = new ArgsParserBuilder().AddFlagOption(fullName, abbreviation, null).Build();

            ArgsParsingResult result = parser.Parser(new[] {"--flag"});
            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("-f"));
        }

        [Fact]
        void should_success_and_get_flag_value_when_add_flag_with_full_name_is_null()
        {
            var abbreviation = 'f';
            var description = "the first flag";

            var parser = new ArgsParserBuilder().AddFlagOption(null, abbreviation, description).Build();

            ArgsParsingResult result = parser.Parser(new[] { "-f" });

            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("-f"));
            Assert.Null(result.Error);
        }

        [Fact]
        void should_success_and_get_flag_value_when_add_flag_with_legal_parameter()
        {
            var fullName = "flag";
            char? abbreviation = 'f';
            var description = "the first flag";
            var parser = new ArgsParserBuilder().AddFlagOption(fullName, abbreviation, description).Build();

            ArgsParsingResult result = parser.Parser(new[] { "--flag" });

            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("-f"));
        }

        [Fact]
        public void should_parse_success_and_can_get_flag_value_when_flag_has_two_valid_names()
        {
            var fullName = "flag";
            var abbrName = 'f';
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
        void should_throw_exception_when_add_flag_with_abbreviation_and_full_name_are_all_null()
        {
            var description = "the first flag";

            Assert.Throws<ArgumentNullException>(() => new ArgsParserBuilder().AddFlagOption(null, null, description).Build());
        }

        [Fact]
        public void should_throw_exception_when_add_flag_with_empty_full_name_and_null_abbr_name()
        {
            Assert.Throws<ArgumentException>(() => new ArgsParserBuilder().AddFlagOption("", null, "description"));
        }

        [Fact]
        public void should_throw_exception_when_add_flag_with_null_full_name_and_empty_abbr_name()
        {
            Assert.Throws<ArgumentException>(() => new ArgsParserBuilder().AddFlagOption(null, default(char), "description"));
        }

        [Fact]
        public void should_throw_exception_when_add_flag_with_invalid_full_name()
        {
            Assert.Throws<ArgumentException>(() => new ArgsParserBuilder().AddFlagOption("-flag", 'f', "description"));
        }

        [Fact]
        public void should_throw_argument_exception_when_add_flag_with_abbreviation_name_is_not_letter()
        {
            Assert.Throws<ArgumentException>(() => new ArgsParserBuilder().AddFlagOption("flag", '3', "description"));
        }

        [Fact]
        public void should_return_false_and_get_error_code_when_parse_parameter_is_invalid()
        {
            var fullName = "flag";
            var abbrName = 'f';
            var description = "the first flag";

            ArgsParser parser = new ArgsParserBuilder().AddFlagOption(fullName, abbrName, description).Build();
            ArgsParsingResult result = parser.Parser(new[] { "-f", "freeValueFlag" });

            Assert.False(result.IsSuccess);
            Assert.Equal(ParsingErrorCode.FreeValueNotSupported, result.Error.Code);
            Assert.Equal("freeValueFlag", result.Error.Trigger);
        }

        [Fact]
        void should_success_add_multiple_flags()
        {
            var fullName1 = "flag";
            var abbreviation1 = 'f';

            var fullName2 = "flagSecond";
            var abbreviation2 = 'S';

            var parser = new ArgsParserBuilder().AddFlagOption(fullName1, abbreviation1, null).AddFlagOption(fullName2, abbreviation2, null).Build();

            ArgsParsingResult result = parser.Parser(new[] {"--flag" });
            Assert.True(result.IsSuccess);
            Assert.True(result.GetFlagValue("-f"));
        }

        [Fact]
        void should_throw_ArgumentException_when_add_with_duplicate_abbreviation_name()
        {
            var fullName1 = "flag";
            var abbreviation1 = 'f';

            var fullName2 = "flagSecond";

            var builder = new ArgsParserBuilder().AddFlagOption(fullName1, abbreviation1, null);
            Assert.Throws<ArgumentException>(() => builder.AddFlagOption(fullName2, abbreviation1, null));
        }

        [Fact]
        void should_throw_ArgumentException_when_add_with_duplicate_full_name()
        {
            var fullName1 = "flag";
            var abbreviation1 = 'f';

            var abbreviation2 = 'S';

            var builder = new ArgsParserBuilder().AddFlagOption(fullName1, abbreviation1, null);
            Assert.Throws<ArgumentException>(() => builder.AddFlagOption(fullName1, abbreviation2, null));
        }

        [Fact]
        void should_success_when_add_mutiple_flags_with_abbreviation_names_are_all_null()
        {
            var fullName1 = "flag";
            var fullName2 = "flagSecond";

            var parser = new ArgsParserBuilder().AddFlagOption(fullName1, null, null).AddFlagOption(fullName2,null,null).Build();
            ArgsParsingResult result = parser.Parser(new[] { "--flag" });
            Assert.True(result.IsSuccess);
        }

        [Fact]
        void should_success_when_add_mutiple_flags_with_full_names_are_all_null()
        {
            var abbreviation1 = 'f';
            var abbreviation2 = 'S';

            var parser = new ArgsParserBuilder().AddFlagOption(null, abbreviation1, null).AddFlagOption(null, abbreviation2, null).Build();
            ArgsParsingResult result = parser.Parser(new[] { "-f" });
            Assert.True(result.IsSuccess);
        }

        [Fact]
        void should_throw_ArgumentException_when_parse_with_parameter_is_null()
        {
            var fullName = "flag";
            var abbreviation = 'f';

            var parser = new ArgsParserBuilder().AddFlagOption(fullName, abbreviation, null).Build();
            Assert.Throws<ArgumentException>(() => parser.Parser(null));
        }

        [Fact]
        void should_throw_ArgumentException_when_parse_with_parameter_has_null()
        {
            var fullName = "flag";
            var abbreviation = 'f';

            var parser = new ArgsParserBuilder().AddFlagOption(fullName, abbreviation, null).Build();
            Assert.Throws<ArgumentException>(() => parser.Parser(new[] {"-f", null}));
        }

        [Fact]
        void should_return_FreeValueNotSupported_error_code_when_parse_with_undefined_flag()
        {
            var fullName = "flag";
            var abbreviationName = 'f';
            var parser = new ArgsParserBuilder().AddFlagOption(fullName, abbreviationName, null).Build();

            ArgsParsingResult result = parser.Parser(new[] { "--second" });
            Assert.False(result.IsSuccess);
            Assert.Equal(ParsingErrorCode.FreeValueNotSupported, result.Error.Code);
            Assert.Equal("--second", result.Error.Trigger);
        }

        [Fact]
        void should_return_DuplicateFlagsInArgs_error_code_when_parse_with_duplicate_parameter()
        {
            var fullName = "flag";
            var abbreviationName = 'f';
            var parser = new ArgsParserBuilder().AddFlagOption(fullName, abbreviationName, null).Build();

            ArgsParsingResult result = parser.Parser(new[] { "-f", "-f" });
            Assert.False(result.IsSuccess);
            Assert.Equal(ParsingErrorCode.DuplicateFlagsInArgs, result.Error.Code);
            Assert.Equal("-f", result.Error.Trigger);
        }

        [Fact]
        void should_return_DuplicateFlagsInArgs_error_code_when_parse_with_both_fullName_and_abbreviationName()
        {
            var fullName = "flag";
            var abbreviationName = 'f';
            var parser = new ArgsParserBuilder().AddFlagOption(fullName, abbreviationName, null).Build();

            ArgsParsingResult result = parser.Parser(new[] { "-f", "--flag" });
            Assert.False(result.IsSuccess);
            Assert.Equal(ParsingErrorCode.DuplicateFlagsInArgs, result.Error.Code);
            Assert.Equal("--flag", result.Error.Trigger);
        }

        [Fact]
        void should_throw_ArgumentException_when_get_flag_value_with_parameter_is_null()
        {
            var fullName = "flag";
            var abbreviationName = 'f';
            var parser = new ArgsParserBuilder().AddFlagOption(fullName, abbreviationName, null).Build();

            ArgsParsingResult result = parser.Parser(new[] { "-f"});
            Assert.True(result.IsSuccess);
            Assert.Throws<ArgumentException>(() => result.GetFlagValue(null));
        }

        [Fact]
        void should_throw_InvalidOperationException_when_get_flag_value_if_IsSucess_is_false()
        {
            var fullName = "flag";
            var abbreviationName = 'f';
            var parser = new ArgsParserBuilder().AddFlagOption(fullName, abbreviationName, null).Build();

            ArgsParsingResult result = parser.Parser(new[] { "-S" });
            Assert.False(result.IsSuccess);
            Assert.Throws<InvalidOperationException>(() => result.GetFlagValue("-f"));
        }

        [Fact]
        void should_throw_ArgumentException_when_get_flag_value_with_undefined_flag_name()
        {
            var fullName = "flag";
            var abbreviationName = 'f';
            var parser = new ArgsParserBuilder().AddFlagOption(fullName, abbreviationName, null).Build();

            ArgsParsingResult result = parser.Parser(new[] { "-f" });
            Assert.True(result.IsSuccess);
            Assert.Throws<ArgumentException>(() => result.GetFlagValue("-S"));
        }

        [Fact]
        void should_throw_ArgumentException_when_get_flag_value_with_invalid_parameter()
        {
            var fullName = "flag";
            var abbreviationName = 'f';
            var parser = new ArgsParserBuilder().AddFlagOption(fullName, abbreviationName, null).Build();

            ArgsParsingResult result = parser.Parser(new[] { "-f" });
            Assert.True(result.IsSuccess);
            Assert.Throws<ArgumentException>(() => result.GetFlagValue("ff"));
        }

        [Fact]
        void should_parse_valid_combined_flags_successfully()
        {
            var fullName1 = "flag";
            var abbreviation1 = 'f';
            var fullName2 = "flagSecond";
            var abbreviation2 = 's';
            var parser = new ArgsParserBuilder()
                .AddFlagOption(fullName1, abbreviation1, String.Empty)
                .AddFlagOption(fullName2, abbreviation2, String.Empty)
                .Build();

            var argsParsingResult = parser.Parser(new[] {"-fs"});

            Assert.True(argsParsingResult.IsSuccess);
            Assert.Null(argsParsingResult.Error);
        }

        [Fact]
        void should_parse_successfully_when_parsing_args_contain_valid_combined_flags_without_order_and_valid_single_flag()
        {
            var fullName1 = "flag";
            var abbreviation1 = 'f';
            var fullName2 = "flagSecond";
            var abbreviation2 = 's';
            var fullName3 = "flagThird";
            var abbreviation3 = 't';
            var parser = new ArgsParserBuilder()
                .AddFlagOption(fullName1, abbreviation1, String.Empty)
                .AddFlagOption(fullName2, abbreviation2, String.Empty)
                .AddFlagOption(fullName3, abbreviation3, String.Empty)
                .Build();

            var argsParsingResult = parser.Parser(new[] {"-sf", "-t"});

            Assert.True(argsParsingResult.IsSuccess);
            Assert.Null(argsParsingResult.Error);

            var argsParsingResult2 = parser.Parser(new[] { "-fs", "-t" });

            Assert.True(argsParsingResult2.IsSuccess);
            Assert.Null(argsParsingResult2.Error);
        }

        [Fact]
        void should_get_value_when_parsing_args_contain_valid_combined_flags_and_valid_single_flag()
        {
            var fullName1 = "flag";
            var abbreviation1 = 'f';
            var fullName2 = "flagSecond";
            var abbreviation2 = 's';
            var fullName3 = "flagThird";
            var abbreviation3 = 't';
            var parser = new ArgsParserBuilder()
                .AddFlagOption(fullName1, abbreviation1, String.Empty)
                .AddFlagOption(fullName2, abbreviation2, String.Empty)
                .AddFlagOption(fullName3, abbreviation3, String.Empty)
                .Build();

            var argsParsingResult = parser.Parser(new[] { "-fs", "-t" });

            Assert.True(argsParsingResult.GetFlagValue("-t"));
        }

        [Fact]
        void should_return_FreeValueNotSupported_error_code_when_combined_flags_contain_undefined_flag_abbreviation()
        {
            var fullName1 = "flag";
            var abbreviation1 = 'f';
            var fullName2 = "flagSecond";
            var abbreviation2 = 's';
            var parser = new ArgsParserBuilder()
                .AddFlagOption(fullName1, abbreviation1, String.Empty)
                .AddFlagOption(fullName2, abbreviation2, String.Empty)
                .Build();

            var argsParsingResult = parser.Parser(new[] {"-fa"});

            Assert.False(argsParsingResult.IsSuccess);
            Assert.Equal(ParsingErrorCode.FreeValueNotSupported, argsParsingResult.Error.Code);
            Assert.Equal("-fa", argsParsingResult.Error.Trigger);
        }

         [Fact]
        void should_return_DuplicateFlagsInArgs_error_code_when_combined_flags_contain_duplicate_flags_abbreviation()
        {
            var fullName1 = "flag";
            var abbreviation1 = 'f';
            var fullName2 = "flagSecond";
            var abbreviation2 = 's';
            var parser = new ArgsParserBuilder()
                .AddFlagOption(fullName1, abbreviation1, String.Empty)
                .AddFlagOption(fullName2, abbreviation2, String.Empty)
                .Build();

            var argsParsingResult = parser.Parser(new[] {"-ff"});

            Assert.False(argsParsingResult.IsSuccess);
            Assert.Equal(ParsingErrorCode.DuplicateFlagsInArgs, argsParsingResult.Error.Code);
            Assert.Equal("-ff", argsParsingResult.Error.Trigger);
        }

        [Fact]
        void should_return_DuplicateFlagsInArgs_error_code_when_combined_flags_contain_same_flag_with_other_flag_in_args()
        {
            var fullName1 = "flag";
            var abbreviation1 = 'f';
            var fullName2 = "flagSecond";
            var abbreviation2 = 's';
            var parser = new ArgsParserBuilder()
                .AddFlagOption(fullName1, abbreviation1, String.Empty)
                .AddFlagOption(fullName2, abbreviation2, String.Empty)
                .Build();

            var argsParsingResult = parser.Parser(new[] { "-fs", "--flag" });

            Assert.False(argsParsingResult.IsSuccess);
            Assert.Equal(ParsingErrorCode.DuplicateFlagsInArgs, argsParsingResult.Error.Code);
            Assert.Equal("--flag", argsParsingResult.Error.Trigger);
        }

        [Fact]
        void should_throw_ArgumentException_when_get_flag_value_with_combined_flag()
        {
            var fullName1 = "flag";
            var abbreviation1 = 'f';
            var fullName2 = "flagSecond";
            var abbreviation2 = 's';
            var parser = new ArgsParserBuilder()
                .AddFlagOption(fullName1, abbreviation1, String.Empty)
                .AddFlagOption(fullName2, abbreviation2, String.Empty)
                .Build();

            ArgsParsingResult result = parser.Parser(new[] { "-fs" });
            Assert.True(result.IsSuccess);
            Assert.Throws<ArgumentException>(() => result.GetFlagValue("fs"));
        }
    }
}