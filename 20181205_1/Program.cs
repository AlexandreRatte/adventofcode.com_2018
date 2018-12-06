using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _20181205_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string polymer = System.IO.File.ReadAllText(@"T:\SVN\adventofcode.com_2018\20181205_1\input.txt");
            bool annihilationsFound = false;

            do
            {
                string reducedPolymerChain = "";
                for (int i = 0; i < polymer.Length; i++)
                {
                    // Let's not exceed the string's length and process that last char in the next loop
                    if (i == polymer.Length - 1)
                    {
                        reducedPolymerChain += polymer[i];
                        continue;
                    }

                    // If the next char is NOT the lowercase or uppercase counterpart of the current char, add it to the reduced chain.
                    if (Math.Abs(polymer[i].CompareTo(polymer[i + 1])) != 32)
                        reducedPolymerChain += polymer[i];
                    // Skip this char and the for() will skip the next.
                    else
                        i += 1;
                }
                // No more reductions possible.
                if (reducedPolymerChain == polymer)
                    annihilationsFound = false;
                else
                    annihilationsFound = true;
                polymer = reducedPolymerChain;
            } while (annihilationsFound);

            Console.WriteLine("Solution is: {0}", polymer.Length);
            Console.ReadKey();
        }
    }
}
