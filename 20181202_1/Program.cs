using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20181202_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var boxIDs = System.IO.File.ReadAllLines(@"T:\SVN\adventofcode.com_2018\20181202_1\input.txt");
            int twice = 0, thrice = 0;
            char[] letters = {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            foreach (string ID in boxIDs)
            {
                bool twiceFound = false, thriceFound = false;
                foreach (char letter in letters)
                {
                    if (ID.Where(c => c == letter).Count() == 2 && !twiceFound)
                    {
                        twice += 1;
                        twiceFound = true;
                    }
                    if (ID.Where(c => c == letter).Count() == 3 && !thriceFound)
                    {
                        thrice += 1;
                        thriceFound = true;
                    }
                    if (twiceFound && thriceFound)
                        break;
                }
            }

            Console.WriteLine("Solution is: {0}", twice * thrice);
            Console.ReadKey();
        }
    }
}
