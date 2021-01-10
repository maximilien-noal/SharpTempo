namespace LibTempo
{
    using RandN;
    using RandN.Compat;

    using System;

    public class TapTempoGame : TapTempo
    {
        private double _secretBPM = 50;

        private readonly RandomShim<StandardRng> _betterRng = RandomShim.Create(StandardRng.Create());

        public TapTempoGame(uint sampleSize, uint resetTimeInSeconds, uint precision) : base(sampleSize, resetTimeInSeconds, precision)
        {
        }

        public override void DisplayBPM(double bpm)
        {
            if (AreBRMEquals(bpm, _secretBPM))
            {
                Console.WriteLine(Resource.Congratulations);
                base.DisplayBPM(bpm);
                _secretBPM = ComputeNewSecretBPM();
            }
            else if (bpm < _secretBPM)
            {
                Console.WriteLine(Resource.Faster);
            }
            else
            {
                Console.WriteLine(Resource.Slower);
            }
        }

        private double ComputeNewSecretBPM() => _betterRng.Next(50, 200);

        private bool AreBRMEquals(double firstBPM, double secondBPM) => BPMToStringWithPrecision(firstBPM) == BPMToStringWithPrecision(secondBPM);
    }
}