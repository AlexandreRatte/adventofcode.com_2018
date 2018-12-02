using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20181201_Part_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var frequencyModulations = System.IO.File.ReadAllLines(@"T:\SVN\adventofcode.com_2018\20181201\input.txt");
            int frequency = 0;
            List<int> resultingFrequencies = new List<int>();

            for (int i = 0; i < frequencyModulations.Count(); i++)
            {
                frequency += Int32.Parse(frequencyModulations[i]);
                if (!resultingFrequencies.Contains(frequency))
                    resultingFrequencies.Add(frequency);
                else
                {
                    Console.WriteLine("Duplicate frequency: {0}", frequency);
                    Console.ReadKey();
                }

                if (i == frequencyModulations.Count() - 1)
                    i = -1;
            }
        }
    }
}
