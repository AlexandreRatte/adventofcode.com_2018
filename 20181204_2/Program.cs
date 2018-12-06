using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _20181204_2
{
    class Sleep
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Sleep(DateTime start)
        {
            this.Start = start;
        }
    }

    class Guard
    {
        public int Id { get; set; }
        public List<Sleep> SleepCycles { get; set; }

        public Guard(int id)
        {
            this.Id = id;
            this.SleepCycles = new List<Sleep>();
        }

        public int GetTotalMinutesAsleep
        {
            get
            {
                int total = 0;
                foreach (var cycle in SleepCycles)
                {
                    TimeSpan ts = cycle.End - cycle.Start;
                    total += (int)ts.TotalMinutes;
                }
                return total;
            }
        }

        public int GetMinuteAsleepMode
        {
            get
            {
                Dictionary<int, int> minuteFrequency = new Dictionary<int, int>();

                foreach (var cycle in SleepCycles)
                {
                    TimeSpan ts = cycle.End - cycle.Start;

                    for (int i = 0; i < ts.TotalMinutes; i++)
                    {
                        if (!minuteFrequency.ContainsKey(cycle.Start.AddMinutes(i).Minute))
                            minuteFrequency[cycle.Start.AddMinutes(i).Minute] = 0;
                        else
                            minuteFrequency[cycle.Start.AddMinutes(i).Minute] += 1;
                    }
                }

                if (minuteFrequency.Count == 0)
                    return Int32.MinValue; // If guard did not sleep, don't return a valid minute.

                return minuteFrequency.OrderByDescending(m => m.Value).First().Key;
            }
        }
        public int GetMinuteAsleepModeFrequency
        {
            get
            {
                Dictionary<int, int> minuteFrequency = new Dictionary<int, int>();

                foreach (var cycle in SleepCycles)
                {
                    TimeSpan ts = cycle.End - cycle.Start;

                    for (int i = 0; i < ts.TotalMinutes; i++)
                    {
                        if (!minuteFrequency.ContainsKey(cycle.Start.AddMinutes(i).Minute))
                            minuteFrequency[cycle.Start.AddMinutes(i).Minute] = 0;
                        else
                            minuteFrequency[cycle.Start.AddMinutes(i).Minute] += 1;
                    }
                }

                if (minuteFrequency.Count == 0)
                    return Int32.MinValue; // If guard did not sleep, don't return a valid minute.

                return minuteFrequency.OrderByDescending(m => m.Value).First().Value;
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var guardLog = System.IO.File.ReadAllLines(@"T:\SVN\adventofcode.com_2018\20181204_1\input.txt");
            Array.Sort(guardLog, StringComparer.InvariantCulture); // Sort the log in lexicographic ascending order.
            List<Guard> guards = new List<Guard>();
            Sleep TempSleep = null;
            Guard CurrentGuard = null;

            foreach (string line in guardLog)
            {
                Regex regex = new Regex(@"^\[([0-9\-:\s]+)\] (falls asleep|wakes up|Guard #(\d+) begins shift)");
                var matches = regex.Match(line);
                string timeStamp = matches.Groups[1].Value;
                string logEntry = matches.Groups[2].Value;

                if (logEntry == "falls asleep")
                    TempSleep = new Sleep(DateTime.Parse(timeStamp));
                else if (logEntry == "wakes up")
                {
                    TempSleep.End = DateTime.Parse(timeStamp);
                    CurrentGuard.SleepCycles.Add(TempSleep);
                }
                else // New guard begins shift!
                {
                    int guardId = Int32.Parse(matches.Groups[3].Value);
                    CurrentGuard = guards.Where(g => g.Id == guardId).SingleOrDefault();

                    if (CurrentGuard == null)
                    {
                        CurrentGuard = new Guard(guardId);
                        guards.Add(CurrentGuard);
                    }
                }
            }

            Guard selectedGuard = guards.OrderByDescending(g => g.GetMinuteAsleepModeFrequency).First();
            Console.WriteLine("Solution is: {0} (minute {1} for guard {2})", selectedGuard.GetMinuteAsleepMode * selectedGuard.Id, selectedGuard.GetMinuteAsleepMode, selectedGuard.Id);
            Console.ReadKey();
        }
    }
}
