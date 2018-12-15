// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
using System.Timers;
using FuncType = System.Func<bool>;

namespace GameLibrary
{
    public static class Notifer
    {
        private struct Duration
        {
            public uint frequency;
            public uint offset;
            public Duration(uint freq, uint offs)
            {
                frequency = freq;
                offset = offs;
            }
        }
        public const uint TIMER_TICK = 100;
        private static List<Tuple<FuncType, Duration>> Durations = new List<Tuple<FuncType, Duration>>();
        private static Timer timer = null;
        private static uint TickCount = 0;

        public static void Subscribe(FuncType action, uint frequency)
        {
            frequency = frequency / TIMER_TICK;
            Durations.Append(new Tuple<FuncType, Duration>(action, new Duration(frequency, TickCount % frequency)));
            if (timer == null)
            {
                timer = new Timer(TIMER_TICK);
                timer.Elapsed += OnTimerTick;
                timer.AutoReset = true;
                timer.Start();
            }
        }

        private static void OnTimerTick(object src, ElapsedEventArgs args)
        {
            TickCount++;

            foreach (Tuple<FuncType, Duration> item in Durations)
            {
                var time = item.Item2;
                if ((TickCount % time.frequency) == time.offset)
                    if (!item.Item1())
                    {
                        Durations.Remove(item);
                    }
            }
        }

    }
}
