// See https://aka.ms/new-console-template for more information

class Program
{
    static void Main(string[] args)
    {
        string filename = args[0];
        string[] lines = File.ReadAllLines(filename);
        
        // PartOne(lines);
        PartTwo(lines);
    }
    
    static void PartOne(string[] lines)
    {
        Dictionary<int, Dictionary<int, int>> grid = new Dictionary<int, Dictionary<int, int>>();

        int stoneValue = 1;
        int sandValue = 2;
            
        foreach (string line in lines)
        {
            string[] parts = line.Split(" -> ");

            List<Coordinate> points = new List<Coordinate>();
            foreach (string part in parts)
            {
                string[] coordinates = part.Split(',');
                points.Add(new Coordinate(coordinates[1], coordinates[0]));
            }
            
            List<Coordinate> stones = new List<Coordinate>();
            for (int x = 0; x < points.Count - 1; x++)
            {
                // Console.WriteLine($"Find path from {points[x].Y}, {points[x].X} to {points[x+1].Y}, {points[x+1].X}");
                stones.AddRange(points[x].ToNext(points[x+1]));
            }

            foreach (Coordinate stone in stones)
            {
                if (!grid.ContainsKey(stone.Y))
                {
                    grid.Add(stone.Y, new Dictionary<int, int>());
                }

                if (!grid[stone.Y].ContainsKey(stone.X))
                {
                    grid[stone.Y].Add(stone.X, stoneValue);
                }
            }
        }
        
        int yMin = 0;
        int yMax = grid.Keys.Max();
        int xMin = -1;
        int xMax = -1;
        foreach (var y in grid)
        {
            if (xMin == -1)
            {
                xMin = y.Value.Keys.Min();
                xMax = y.Value.Keys.Max();
                continue;
            }
                
            if (y.Value.Keys.Min() < xMin)
            {
                xMin = y.Value.Keys.Min();
            }
                
            if (y.Value.Keys.Max() > xMax)
            {
                xMax = y.Value.Keys.Max();
            }
        }
            
        Console.WriteLine($"Grid boundaries: {yMin},{xMin} - {yMax},{xMax}");

        for (int y = yMin; y <= yMax; y++)
        {
            Console.Write($"{y} ");
            for (int x = xMin; x <= xMax; x++)
            {
                if (grid.ContainsKey(y) && grid[y].ContainsKey(x))
                {
                    if (grid[y][x] == stoneValue)
                    {
                        Console.Write('X');
                    } else if (grid[y][x] == sandValue)
                    {
                        Console.Write('o');
                    }
                }
                else
                {
                    Console.Write('.');
                }
            }
            Console.WriteLine();
        }

        // keep starting with new sand particle
        int particles = 0;
        while (true)
        {
            // position sand, then start flowing it down
            Coordinate sand = new Coordinate(0, 500);
            while (true)
            {
                // are we off the map?
                if (sand.Y > yMax)
                {
                    Console.WriteLine($"Sand reached the void at {sand.Y},{sand.X}");
                    break;
                }
                
                // can it fall down?
                Coordinate down = new Coordinate(sand.Y + 1, sand.X);
                if (!grid.ContainsKey(down.Y) || !grid[down.Y].ContainsKey(down.X))
                {
                    Console.WriteLine($"Can fall down to {down.Y},{down.X}");
                    sand = down;
                    continue;
                }
            
                // can it fall left?
                Coordinate left = new Coordinate(sand.Y + 1, sand.X-1);
                if (!grid.ContainsKey(left.Y) || !grid[left.Y].ContainsKey(left.X))
                {
                    Console.WriteLine($"Can fall left to {left.Y},{left.X}");
                    sand = left;
                    continue;
                }
            
                // can it fall right?
                Coordinate right = new Coordinate(sand.Y + 1, sand.X+1);
                if (!grid.ContainsKey(right.Y) || !grid[right.Y].ContainsKey(right.X))
                {
                    Console.WriteLine($"Can fall right to {right.Y},{right.X}");
                    sand = right;
                    continue;
                }
            
                // trapped
                Console.WriteLine($"Trapped at {sand.Y},{sand.X}");
                if (!grid.ContainsKey(sand.Y))
                {
                    grid.Add(sand.Y, new Dictionary<int, int>());
                }
                grid[sand.Y].Add(sand.X, sandValue);
                particles++;
                break;
            }
        
            // are we off the map?
            if (sand.Y > yMax)
            {
                Console.WriteLine($"Sand reached the void at {sand.Y},{sand.X}");
                break;
            }
        }

        // for (int y = yMin; y <= yMax; y++)
        // {
        //     Console.Write($"{y} ");
        //     for (int x = xMin; x <= xMax; x++)
        //     {
        //         if (grid.ContainsKey(y) && grid[y].ContainsKey(x))
        //         {
        //             if (grid[y][x] == stoneValue)
        //             {
        //                 Console.Write('X');
        //             } else if (grid[y][x] == sandValue)
        //             {
        //                 Console.Write('o');
        //             }
        //         }
        //         else
        //         {
        //             Console.Write('.');
        //         }
        //     }
        //     Console.WriteLine();
        // }
        
        Console.WriteLine($"Answer #1 is {particles}");
    }
    
    static void PartTwo(string[] lines)
    {
        Dictionary<int, Dictionary<int, int>> grid = new Dictionary<int, Dictionary<int, int>>();

        int stoneValue = 1;
        int sandValue = 2;
            
        foreach (string line in lines)
        {
            string[] parts = line.Split(" -> ");

            List<Coordinate> points = new List<Coordinate>();
            foreach (string part in parts)
            {
                string[] coordinates = part.Split(',');
                points.Add(new Coordinate(coordinates[1], coordinates[0]));
            }
            
            List<Coordinate> stones = new List<Coordinate>();
            for (int x = 0; x < points.Count - 1; x++)
            {
                // Console.WriteLine($"Find path from {points[x].Y}, {points[x].X} to {points[x+1].Y}, {points[x+1].X}");
                stones.AddRange(points[x].ToNext(points[x+1]));
            }

            foreach (Coordinate stone in stones)
            {
                if (!grid.ContainsKey(stone.Y))
                {
                    grid.Add(stone.Y, new Dictionary<int, int>());
                }

                if (!grid[stone.Y].ContainsKey(stone.X))
                {
                    grid[stone.Y].Add(stone.X, stoneValue);
                }
            }
        }
        
        int yMin = 0;
        int yMax = grid.Keys.Max() + 2;
        int xMin = -1;
        int xMax = -1;
        foreach (var y in grid)
        {
            if (xMin == -1)
            {
                xMin = y.Value.Keys.Min();
                xMax = y.Value.Keys.Max();
                continue;
            }
                
            if (y.Value.Keys.Min() < xMin)
            {
                xMin = y.Value.Keys.Min();
            }
                
            if (y.Value.Keys.Max() > xMax)
            {
                xMax = y.Value.Keys.Max();
            }
        }
            
        Console.WriteLine($"Grid boundaries: {yMin},{xMin} - {yMax},{xMax}");

        for (int y = yMin; y <= yMax; y++)
        {
            Console.Write($"{y} ");
            for (int x = xMin; x <= xMax; x++)
            {
                if (grid.ContainsKey(y) && grid[y].ContainsKey(x))
                {
                    if (grid[y][x] == stoneValue)
                    {
                        Console.Write('X');
                    } else if (grid[y][x] == sandValue)
                    {
                        Console.Write('o');
                    }
                }
                else
                {
                    Console.Write('.');
                }
            }
            Console.WriteLine();
        }

        // keep starting with new sand particle
        int particles = 0;
        while (true)
        {
            // position sand, then start flowing it down
            Coordinate sand = new Coordinate(0, 500);
            
            // is the source blocked?
            if (grid.ContainsKey(sand.Y) && grid[sand.Y].ContainsKey(sand.X))
            {
                Console.WriteLine($"Sand blocked the source at {sand.Y},{sand.X}");
                break;
            }
            
            while (true)
            {
                // can it fall down?
                Coordinate down = new Coordinate(sand.Y + 1, sand.X);
                if (down.Y < yMax && (!grid.ContainsKey(down.Y) || !grid[down.Y].ContainsKey(down.X)))
                {
                    //Console.WriteLine($"Can fall down to {down.Y},{down.X}");
                    sand = down;
                    continue;
                }
            
                // can it fall left?
                Coordinate left = new Coordinate(sand.Y + 1, sand.X-1);
                if (left.Y < yMax && (!grid.ContainsKey(left.Y) || !grid[left.Y].ContainsKey(left.X)))
                {
                    //Console.WriteLine($"Can fall left to {left.Y},{left.X}");
                    sand = left;
                    continue;
                }
            
                // can it fall right?
                Coordinate right = new Coordinate(sand.Y + 1, sand.X+1);
                if (right.Y < yMax && (!grid.ContainsKey(right.Y) || !grid[right.Y].ContainsKey(right.X)))
                {
                    //Console.WriteLine($"Can fall right to {right.Y},{right.X}");
                    sand = right;
                    continue;
                }
            
                // trapped
                //Console.WriteLine($"Trapped at {sand.Y},{sand.X}");
                if (!grid.ContainsKey(sand.Y))
                {
                    grid.Add(sand.Y, new Dictionary<int, int>());
                }
                grid[sand.Y].Add(sand.X, sandValue);
                particles++;
                break;
            }
        
            // are we off the map?
            if (sand.Y > yMax)
            {
                Console.WriteLine($"Sand reached the void at {sand.Y},{sand.X}");
                break;
            }
        }

        for (int y = yMin; y <= yMax; y++)
        {
            Console.Write($"{y} ");
            for (int x = xMin; x <= xMax; x++)
            {
                if (grid.ContainsKey(y) && grid[y].ContainsKey(x))
                {
                    if (grid[y][x] == stoneValue)
                    {
                        Console.Write('X');
                    } else if (grid[y][x] == sandValue)
                    {
                        Console.Write('o');
                    }
                }
                else
                {
                    Console.Write('.');
                }
            }
            Console.WriteLine();
        }
        
        Console.WriteLine($"Answer #2 is {particles}");
    }

    public class Coordinate
    {
        public readonly int Y;
        public readonly int X;
        
        public Coordinate(string y, string x)
        {
            Y = Convert.ToInt32(y);
            X = Convert.ToInt32(x);
        }
        
        public Coordinate(int y, int x)
        {
            Y = y;
            X = x;
        }

        public List<Coordinate> ToNext(Coordinate next)
        {
            List<Coordinate> path = new List<Coordinate>();

            if (Y == next.Y)
            {
                // loop over X values
                for (int x = Math.Min(X, next.X); x <= Math.Max(X, next.X); x++)
                {
                    // Console.WriteLine($"  - {Y}, {x}");
                    path.Add(new Coordinate(Y, x));
                }
            }

            if (X == next.X)
            {
                // loop over Y values
                for (int y = Math.Min(Y, next.Y); y <= Math.Max(Y, next.Y); y++)
                {
                    // Console.WriteLine($"  - {y}, {X}");
                    path.Add(new Coordinate(y, X));
                }
            }
            
            return path;
        }
    }
}
