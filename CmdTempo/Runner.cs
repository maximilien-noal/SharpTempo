namespace CmdTempo
{
    using LibTempo;

    internal static class Runner
    {
        internal static void RunOptions(Options options)
        {
            if (options.IsGamingMode)
            {
                new TapTempoGame(options.SampleSize, options.ResetTime, options.Precision).Run();
            }
            else
            {
                new TapTempo(options.SampleSize, options.ResetTime, options.Precision).Run();
            }
        }
    }
}