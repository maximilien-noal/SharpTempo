namespace TestTempo
{
    using CommandLine;

    using FluentAssertions;

    using LibTempo;

    using Xunit;

    public class OptionsUnitTests
    {
        [Fact]
        public void GotMotonoticClock()
        {
            System.Diagnostics.Stopwatch.IsHighResolution.Should().BeTrue();
        }

        [Fact]
        public void InvalidArsShouldReturnDefaultOptions()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { "0", "0" });
            result.Tag.Should().Be(ParserResultType.NotParsed);
        }

        [Fact]
        public void ZeroArgsShouldReturnDefaultOptions()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { "1" });
            result.Tag.Should().Be(ParserResultType.NotParsed);
        }

        [Fact]
        public void HelpSwitchShouldExit()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { "--help" });
            result.Tag.Should().Be(ParserResultType.NotParsed);
        }

        [Fact]
        public void ShortHelpSwitchShouldExit()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { "--h" });
            result.Tag.Should().Be(ParserResultType.NotParsed);
        }

        [Fact]
        public void VersionSwitchShouldExit()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { "--version" });
            result.Tag.Should().Be(ParserResultType.NotParsed);
        }

        [Fact]
        public void ShortVersionSwitchShouldExit()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { "-v" });
            result.Tag.Should().Be(ParserResultType.NotParsed);
        }

        [Fact]
        public void PrecisionSwitchShouldReturnTheRightValue()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { $"-s {Options.DefaultSampleSize}", $"-r {Options.DefaultResetTime}", "--precision 2" })
                .WithParsed((o) => o.Precision.Should().Be(2));
        }

        [Fact]
        public void ShortPrecisionSwitchShouldReturnTheRightValue()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { $"-s {Options.DefaultSampleSize}", $"-r {Options.DefaultResetTime}", "-p 2" })
                .WithParsed((o) => o.Precision.Should().Be(2));
        }

        [Fact]
        public void WrongPrecisionSwitchShouldReturnTheDefaultValue()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { $"-s {Options.DefaultSampleSize}", $"-r {Options.DefaultResetTime}", "--precision 0" })
                .WithParsed((o) => o.Precision.Should().Be(Options.DefaultPrecision));
        }

        [Fact]
        public void OutOfBoundPrecisionSwitchShouldReturnTheMaxValue()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { $"-s {Options.DefaultSampleSize}", $"-r {Options.DefaultResetTime}", "--precision 10" })
                .WithParsed((o) => o.Precision.Should().Be(Options.MaxPrecision));
        }

        [Fact]
        public void SampleSizeSwitchShouldReturnTheRightValue()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { "--sample-size 2", $"-r {Options.DefaultResetTime}", $"-p {Options.DefaultPrecision}" })
                .WithParsed((o) => o.SampleSize.Should().Be(2));
        }

        [Fact]
        public void ShortSampleSizeSwitchShouldReturnTheRightValue()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { "-s 2", $"-r {Options.DefaultResetTime}", $"-p {Options.DefaultPrecision}" })
                .WithParsed((o) => o.SampleSize.Should().Be(2));
        }

        [Fact]
        public void WrongSampleSizeSwitchShouldReturnTheRightValue()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { "-s 0", $"-r {Options.DefaultResetTime}", $"-p {Options.DefaultPrecision}" })
                .WithParsed((o) => o.SampleSize.Should().Be(Options.DefaultSampleSize));
        }

        [Fact]
        public void ResetTimeSwitchShouldReturnTheRightValue()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { $"-s {Options.DefaultSampleSize}", "--reset-time 10", $"-p {Options.DefaultPrecision}" })
                .WithParsed((o) => o.ResetTime.Should().Be(10));
        }

        [Fact]
        public void ShortResetTimeSwitchShouldReturnTheRightValue()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { $"-s {Options.DefaultSampleSize}", "-r 10", $"-p {Options.DefaultPrecision}" })
                .WithParsed((o) => o.ResetTime.Should().Be(10));
        }

        [Fact]
        public void WrongResetTimeSwitchShouldReturnTheDefaultValue()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { $"-s {Options.DefaultSampleSize}", "-r -10", $"-p {Options.DefaultPrecision}" })
                .WithParsed((o) => o.ResetTime.Should().Be(Options.DefaultPrecision));
        }

        [Fact]
        public void AllSwitchesShouldReturnTheRightValues()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { "-s 6", "--reset-time 10", "--precision 2", "--game" })
                .WithParsed((o) => o.Should().BeEquivalentTo(new Options(true, 6, 10, 2)));
        }

        [Fact]
        public void GameSwitchShouldReturnTheRightValue()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { "--game", "--reset-time 10", "--precision 2", "-s 2" })
                .WithParsed((o) => o.IsGamingMode.Should().BeTrue());
        }

        [Fact]
        public void ShortGameSwitchShouldReturnTheRightValue()
        {
            var result = Parser.Default.ParseArguments<Options>(args: new string[] { "-g", "--reset-time 10", "--precision 2", "-s 2" })
                .WithParsed((o) => o.IsGamingMode.Should().BeTrue());
        }
    }
}