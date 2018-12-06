using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20181205_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string polymer = System.IO.File.ReadAllText(@"T:\SVN\adventofcode.com_2018\20181205_1\input.txt");
            int shortestPolymer = Int32.MaxValue;
            var start = DateTime.Now;

            foreach (char character in "abcdefghijklmnopqrstuvwxyz")
            {
                string modifiedPolymer = polymer.Replace(character.ToString(), string.Empty).Replace(((char)(character - 32)).ToString(), string.Empty);
                bool annihilationsFound = false;
                do
                {
                    string reducedPolymerChain = "";
                    for (int i = 0; i < modifiedPolymer.Length; i++)
                    {
                        // Let's not exceed the string's length and process that last char in the next loop
                        if (i == modifiedPolymer.Length - 1)
                        {
                            reducedPolymerChain += modifiedPolymer[i];
                            continue;
                        }

                        // If the next char is NOT the lowercase or uppercase counterpart of the current char, add it to the reduced chain.
                        if (Math.Abs(modifiedPolymer[i].CompareTo(modifiedPolymer[i + 1])) != 32)
                            reducedPolymerChain += modifiedPolymer[i];
                        // Skip this char and the for() will skip the next.
                        else
                            i += 1;
                    }
                    // No more reductions possible.
                    if (reducedPolymerChain == modifiedPolymer)
                        annihilationsFound = false;
                    else
                        annihilationsFound = true;
                    modifiedPolymer = reducedPolymerChain;
                } while (annihilationsFound);

                if (modifiedPolymer.Length < shortestPolymer)
                    shortestPolymer = modifiedPolymer.Length;
            }

            var end = DateTime.Now;
            Console.WriteLine("Solution is: {0} and it took {1} seconds to complete.", shortestPolymer, (end-start).TotalSeconds);
            Console.ReadKey();
        }
    }
}
