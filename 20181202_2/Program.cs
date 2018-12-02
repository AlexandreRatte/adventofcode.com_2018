using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20181202_2
{
    class Program
    {
        static string GetMatchingBoxes(string first, string second)
        {
            int count = 0;
            int position = int.MinValue;
            for (int i=0;i<first.Length; i++)
                if (first[i] != second[i])
                {
                    count += 1;
                    position = i;
                }

            if (count != 1)
                return null;
            else
                return first.Substring(0,position) + first.Substring(position+1);
        }

        static void Main(string[] args)
        {
            var boxIDs = System.IO.File.ReadAllLines(@"T:\SVN\adventofcode.com_2018\20181202_1\input.txt");
            foreach (string firstID in boxIDs)
                foreach (string secondID in boxIDs.Where(b => b != firstID))
                {
                    string solution = GetMatchingBoxes(firstID, secondID);
                    if (solution != null)
                    {
                        Console.WriteLine("Solution is: {0}", solution);
                        Console.ReadKey();
                        return;
                    }
                }
        }
    }
}
