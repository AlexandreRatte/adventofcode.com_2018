using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20181201
{
    class Program
    {
        static void Main(string[] args)
        {
            var frequencyModulations = System.IO.File.ReadAllLines(@"T:\SVN\adventofcode.com_2018\20181201\input.txt");
            int frequency = 0;

            foreach (var modulation in frequencyModulations)
                frequency += Int32.Parse(modulation);

            Console.WriteLine("Resulting frequency: {0}", frequency);
            Console.ReadKey();
        }
    }
}
