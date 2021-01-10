namespace LibTempo
{
    using CommandLine;

    public record Options
    {
        public const uint DefaultSampleSize = 5;
        public const uint DefaultResetTime = 5;
        public const uint DefaultPrecision = 0;
        public const uint MaxPrecision = 5;

        [Option('g', "game", Required = false, Default = false, HelpText = nameof(IsGamingMode), ResourceType = typeof(Resource))]
        public bool IsGamingMode { get; }

        [Option('s', "sample-size", Required = true, Default = DefaultSampleSize, HelpText = nameof(SampleSize), ResourceType = typeof(Resource))]
        public uint SampleSize { get; }

        [Option('r', "reset-time", Required = true, Default = DefaultResetTime, HelpText = nameof(ResetTime), ResourceType = typeof(Resource))]
        public uint ResetTime { get; }

        [Option('p', "precision", Required = true, Default = DefaultPrecision, HelpText = nameof(Precision), ResourceType = typeof(Resource))]
        public uint Precision { get; }

        public Options(bool isGamingMode, uint sampleSize, uint resetTime, uint precision)
        {
            IsGamingMode = isGamingMode;
            SampleSize = sampleSize == 0 ? DefaultSampleSize : sampleSize;
            ResetTime = resetTime == 0 ? DefaultResetTime : resetTime;
            Precision = precision > MaxPrecision ? MaxPrecision : precision == 0 ? DefaultPrecision : precision;
        }
    }
}