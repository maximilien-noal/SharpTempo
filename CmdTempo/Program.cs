using CmdTempo;

using CommandLine;
using CommandLine.Text;

using LibTempo;

// Set sentence builder to localizable
SentenceBuilder.Factory = () => new LocalizableSentenceBuilder();
Parser.Default.ParseArguments<Options>(args).WithParsed((options) => Runner.RunOptions(options));