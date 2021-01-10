using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace LibTempo
{
    public class TapTempo
    {
        private readonly TimeSpan _resetTimeInSeconds;
        private readonly uint _sampleSize;
        private readonly uint _precision;
        private readonly Queue<long> _hitTimePoints = new Queue<long>();

        public TapTempo(uint sampleSize, uint resetTimeInSeconds, uint precision)
        {
            _resetTimeInSeconds = TimeSpan.FromSeconds(resetTimeInSeconds);
            _precision = precision;
            _sampleSize = sampleSize;
        }

        public void Run()
        {
            Console.WriteLine(Resource.HitEnterForEachTempoOrQToQuit);
            var shouldContinue = true;
            while (shouldContinue)
            {
                int i;
                do
                {
                    i = Console.Read();
                    if (Convert.ToChar(i) == 'q' || i == -1)
                    {
                        shouldContinue = false;
                        if (i == -1)
                        {
                            Console.WriteLine(Resource.ByeBye);
                            break;
                        }
                    }
                } while (i != 10);
                var currentTime = GetCurrentTime();
                if (shouldContinue)
                {
                    // Reset if the hit time is too big.
                    if (!_hitTimePoints.IsEmpty() && IsResetTimeElapsed(currentTime, _hitTimePoints.Back()))
                    {
                        _hitTimePoints.Clear();
                    }

                    _hitTimePoints.Enqueue(currentTime);
                    if (_hitTimePoints.Count > 1)
                    {
                        var bpm = ComputeBPM(_hitTimePoints.Back(), _hitTimePoints.Front(), _hitTimePoints.Count - 1);
                        DisplayBPM(bpm);
                    }
                    else
                    {
                        Console.WriteLine(Resource.HitEnterToStartBpmComputation);
                    }

                    while (_hitTimePoints.Count > _sampleSize)
                    {
                        _ = _hitTimePoints.Dequeue();
                    }
                }
            }
        }

        private static double ComputeBPM(long currentTime, long lastTime, long occurenceCount)
        {
            if (occurenceCount <= 0)
            {
                occurenceCount = 1;
            }
            var elapsedTime = GetElapsedTime(currentTime, lastTime);
            var bpm = double.PositiveInfinity;
            if (elapsedTime > 0)
            {
                var meanTime = elapsedTime / occurenceCount;
                bpm = 60d / meanTime;
            }
            return bpm;
        }

        private static long GetCurrentTime() => Stopwatch.GetTimestamp();

        public virtual void DisplayBPM(double bpm) => Console.WriteLine($"Tempo: {BPMToStringWithPrecision(bpm)} bpm");

        protected string BPMToStringWithPrecision(double bpm) => bpm.ToString($"G{_precision}", CultureInfo.CurrentCulture);

        private bool IsResetTimeElapsed(long currentTime, long lastTime) => GetElapsedTime(currentTime, lastTime) > _resetTimeInSeconds.Ticks;

        private static double GetElapsedTime(long currentTime, long lastTime) => (TimeSpan.FromTicks(currentTime) - TimeSpan.FromTicks(lastTime)).TotalSeconds;
    }
}