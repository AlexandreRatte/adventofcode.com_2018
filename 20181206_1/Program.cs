using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _20181206_1
{
    class Distances
    {
        public Dictionary<Point, int> DistancesGrid { get; set; }
        public Point Origin { get; set; }
        public char Id { get; set; }

        public Distances(char id, Point origin, int minX, int maxX, int minY, int maxY)
        {
            this.Id = id;
            this.Origin = origin;
            DistancesGrid = new Dictionary<Point, int>();
            CalculateManhattanDistances(minX, maxX, minY, maxY);
        }

        private void CalculateManhattanDistances(int minX, int maxX, int minY, int maxY)
        {
            for (int x = minX; x <= maxX; x++)
                for (int y = minY; y <= maxY; y++)
                {
                    int XDist = Math.Max(Origin.X, x) - Math.Min(Origin.X, x);
                    int YDist = Math.Max(Origin.Y, y) - Math.Min(Origin.Y, y);
                    DistancesGrid.Add(new Point(x, y), XDist + YDist);
                }
        }
    }

    class Grid
    {
        public Dictionary<Point, char> OwnershipGrid { get; set; } // Location, Owner
        public Dictionary<Point, int> DistanceGrid { get; set; } // Location, Distance

        public Grid()
        {
            OwnershipGrid = new Dictionary<Point, char>();
            DistanceGrid = new Dictionary<Point, int>();
        }

        public void Add(Distances d)
        {
            foreach (var p in d.DistancesGrid)
            {
                if (!OwnershipGrid.ContainsKey(p.Key)) // Add the new key
                {
                    OwnershipGrid.Add(p.Key, d.Id);
                    DistanceGrid.Add(p.Key, d.DistancesGrid[p.Key]);
                }

                else if (d.DistancesGrid[p.Key] < DistanceGrid[p.Key]) // Update existing key with a smaller value
                {
                    OwnershipGrid[p.Key] = d.Id;
                    DistanceGrid[p.Key] = d.DistancesGrid[p.Key];
                }

                else if (d.DistancesGrid[p.Key] == DistanceGrid[p.Key]) // Equidistant for 2 points
                    OwnershipGrid[p.Key] = '.';
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var coordinates = System.IO.File.ReadAllLines(@"T:\SVN\adventofcode.com_2018\20181206_1\input.txt");
            int minX = Int32.Parse(coordinates.OrderBy(x => Int32.Parse(x.Split(',')[0].Trim())).First().Split(',')[0].Trim()); // Kill me!
            int maxX = Int32.Parse(coordinates.OrderByDescending(x => Int32.Parse(x.Split(',')[0].Trim())).First().Split(',')[0].Trim());
            int minY = Int32.Parse(coordinates.OrderBy(x => Int32.Parse(x.Split(',')[1].Trim())).First().Split(',')[1].Trim());
            int maxY = Int32.Parse(coordinates.OrderByDescending(x => Int32.Parse(x.Split(',')[1].Trim())).First().Split(',')[1].Trim());
            Grid grid = new Grid();
            string Ids = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int SelectedId = 0;

            foreach (string coordinate in coordinates)
            {
                Point coord = new Point(Int32.Parse(coordinate.Split(',')[0].Trim()), Int32.Parse(coordinate.Split(',')[1].Trim()));
                Distances distances = new Distances(Ids[SelectedId], coord, minX, maxX, minY, maxY);
                grid.Add(distances);
                SelectedId++;
            }

            string infiniteCoords = "";
            for (int x=minX; x<=maxX; x++)
                for (int y=minY;y<=maxY;y++)
                {
                    if (x == minX || x == maxX || y == minY || y == maxY)
                        if (!infiniteCoords.Contains(grid.OwnershipGrid[new Point(x, y)]) && grid.OwnershipGrid[new Point(x, y)] != '.')
                            infiniteCoords += grid.OwnershipGrid[new Point(x, y)];
                    //Console.SetCursorPosition(x, y);
                    //Console.Write(grid.OwnershipGrid[new Point(x, y)]);
                }

            int Solution = int.MinValue;
            foreach (var coord in grid.OwnershipGrid)
            {
                if (infiniteCoords.Contains(coord.Value))
                    continue;

                int area = grid.OwnershipGrid.Where(g => g.Value == coord.Value).Count();

                if (Solution < area)
                    Solution = area;
            }

            Console.WriteLine("");
            Console.WriteLine("Solution is: {0}", Solution);
            Console.ReadKey();
        }
    }
}
