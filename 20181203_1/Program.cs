using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Drawing;


namespace _20181203_1
{
    class Rectangle
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }

        public Rectangle(int x1, int y1, int x2, int y2)
        {
            this.X1 = x1;
            this.Y1 = y1;
            this.X2 = x2;
            this.Y2 = y2;
        }

        public int Width
        {
            get
            {
                return X2 - X1;
            }
        }

        public int Height
        {
            get
            {
                return Y2 - Y1;
            }
        }
    }

    class Claim
    {
        public int Id { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Rectangle Location { get; set; }

        public Claim(string claim)
        {
            // Parse #123 @ 3,2: 5x4
            Regex regex = new Regex(@"^#(\d+) @ (\d+),(\d+): (\d+)x(\d+)");
            var matches = regex.Match(claim);
            this.Id = Int32.Parse(matches.Groups[1].Value);
            this.Left = Int32.Parse(matches.Groups[2].Value);
            this.Top = Int32.Parse(matches.Groups[3].Value);
            this.Width = Int32.Parse(matches.Groups[4].Value);
            this.Height = Int32.Parse(matches.Groups[5].Value);
            this.Location = new Rectangle(this.Left, this.Top, this.Left + this.Width, this.Top + this.Height);
        }

        public Rectangle GetOverlappingRectangle(Claim other)
        {
            Rectangle overlap = new Rectangle(Math.Max(this.Location.X1, other.Location.X1),
                                              Math.Max(this.Location.Y1, other.Location.Y1),
                                              Math.Min(this.Location.X2, other.Location.X2),
                                              Math.Min(this.Location.Y2, other.Location.Y2));

            if (overlap.Width < 0 || overlap.Height < 0)
                return null;

            return overlap;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var claimStrings = System.IO.File.ReadAllLines(@"T:\SVN\adventofcode.com_2018\20181203_1\input.txt");
            List<Claim> Claims = new List<Claim>();
            // Assign a dummy value to the dictionary, we're just interested in the key count.
            Dictionary<Point, bool> overlappingInches = new Dictionary<Point, bool>();

            foreach (string claim in claimStrings)
                Claims.Add(new Claim(claim));

            for (int i = 0; i < Claims.Count; i++)
            {
                for (int j = 0; j < Claims.Count ; j++)
                {
                    if (i == j)
                        continue; // Skip comparing the same claims with eachother.

                    Rectangle overlap = Claims[i].GetOverlappingRectangle(Claims[j]);

                    if (overlap != null)
                        for (int x=overlap.X1; x<overlap.X2; x++)
                            for (int y = overlap.Y1; y < overlap.Y2; y++)
                            {
                                // For each overlapping point, add it to the dictionary.
                                Point p = new Point(x, y);
                                if (!overlappingInches.ContainsKey(p))
                                    overlappingInches[p] = true; 
                            }
                }
            }

            Console.WriteLine("Solution is: {0}", overlappingInches.Count);
            Console.ReadKey();
        }
    }
}
