// See https://aka.ms/new-console-template for more information

using aoc2022day12;

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
        int rows = lines.Length;
        int cols = lines[0].Length;
        int[,] heights = new int[cols, rows];
        int row, col;
        
        Coordinate? start = null;
        Coordinate? finish = null;

        List<Coordinate> vertices = new List<Coordinate>();
        List<Tuple<Coordinate, Coordinate>> edges = new List<Tuple<Coordinate, Coordinate>>();

        for (row = 0; row < rows; row++)
        {
            for (col = 0; col < cols; col++)
            {
                // detect start
                char letter = lines[row][col];
                if (letter == 'S')
                {
                    start = new Coordinate(col, row);
                    heights[col, row] = GetHeight('a');
                    continue;
                }

                // detect finish
                if (letter == 'E')
                {
                    finish = new Coordinate(col, row);
                    heights[col, row] = GetHeight('z');
                    continue;
                }

                // init heights
                heights[col, row] = GetHeight(letter);
            }
        }

        if (start == null || finish == null)
        {
            throw new Exception("Start or finish not found");
        }
        
        // add vertices
        for (row = 0; row < rows; row++)
        {
            for (col = 0; col < cols; col++)
            {
                vertices.Add(new Coordinate(col, row));
            }
        }

        // add edges
        for (row = 0; row < rows; row++)
        {
            for (col = 0; col < cols; col++)
            {
                Coordinate current = new Coordinate(col, row);
                // Console.WriteLine($"Traversion {current.X()},{current.Y()} ({heights[col, row]})");
                List<Coordinate> neighbors = new List<Coordinate>();

                neighbors.Add(new Coordinate(col, row - 1)); // up
                neighbors.Add(new Coordinate(col + 1, row)); // right
                neighbors.Add(new Coordinate(col, row + 1)); // down
                neighbors.Add(new Coordinate(col - 1, row)); // left

                foreach (Coordinate neighbor in neighbors)
                {
                    if (
                        neighbor.X() >= 0 &&
                        neighbor.Y() >= 0 &&
                        neighbor.X() < cols &&
                        neighbor.Y() < rows &&
                        heights[neighbor.X(), neighbor.Y()] <= heights[col, row] + 1
                    )
                    {
                        // Console.WriteLine($"- Add neighbor {neighbor.X()},{neighbor.Y()} ({heights[neighbor.X(), neighbor.Y()]})");
                        edges.Add(Tuple.Create(current, neighbor));
                    }
                }
            }
        }
        
        Graph<Coordinate> graph = new Graph<Coordinate>(vertices, edges);

        // var vertex = new Coordinate(2, 0);
        // foreach (var neighbor in graph.AdjacencyList[vertex])
        // {
        //     Console.WriteLine($"Reachable from 2,0 => {neighbor.X()}, {neighbor.Y()}");
        // }

        var shortestPath = ShortestPathFunction(graph, start);

        Console.WriteLine(string.Join(", ", shortestPath(finish)));
        Console.WriteLine($"Solution length: {shortestPath(finish).Count() - 1}");
    }
    
    static void PartTwo(string[] lines)
    {
        int rows = lines.Length;
        int cols = lines[0].Length;
        int[,] heights = new int[cols, rows];
        int row, col;
        
        List<Coordinate> starts = new List<Coordinate>();
        Coordinate? finish = null;

        List<Coordinate> vertices = new List<Coordinate>();
        List<Tuple<Coordinate, Coordinate>> edges = new List<Tuple<Coordinate, Coordinate>>();

        for (row = 0; row < rows; row++)
        {
            for (col = 0; col < cols; col++)
            {
                // detect starts
                char letter = lines[row][col];
                if (letter == 'S' || letter == 'a')
                {
                    starts.Add(new Coordinate(col, row));
                    heights[col, row] = GetHeight('a');
                    continue;
                }

                // detect finish
                if (letter == 'E')
                {
                    finish = new Coordinate(col, row);
                    heights[col, row] = GetHeight('z');
                    continue;
                }

                // init heights
                heights[col, row] = GetHeight(letter);
            }
        }

        if (starts.Count == 0 || finish == null)
        {
            throw new Exception("Start or finish not found");
        }
        
        // add vertices
        for (row = 0; row < rows; row++)
        {
            for (col = 0; col < cols; col++)
            {
                vertices.Add(new Coordinate(col, row));
            }
        }

        // add edges
        for (row = 0; row < rows; row++)
        {
            for (col = 0; col < cols; col++)
            {
                Coordinate current = new Coordinate(col, row);
                // Console.WriteLine($"Traversion {current.X()},{current.Y()} ({heights[col, row]})");
                List<Coordinate> neighbors = new List<Coordinate>();

                neighbors.Add(new Coordinate(col, row - 1)); // up
                neighbors.Add(new Coordinate(col + 1, row)); // right
                neighbors.Add(new Coordinate(col, row + 1)); // down
                neighbors.Add(new Coordinate(col - 1, row)); // left

                foreach (Coordinate neighbor in neighbors)
                {
                    if (
                        neighbor.X() >= 0 &&
                        neighbor.Y() >= 0 &&
                        neighbor.X() < cols &&
                        neighbor.Y() < rows &&
                        heights[neighbor.X(), neighbor.Y()] <= heights[col, row] + 1
                    )
                    {
                        // Console.WriteLine($"- Add neighbor {neighbor.X()},{neighbor.Y()} ({heights[neighbor.X(), neighbor.Y()]})");
                        edges.Add(Tuple.Create(current, neighbor));
                    }
                }
            }
        }
        
        Graph<Coordinate> graph = new Graph<Coordinate>(vertices, edges);

        // var vertex = new Coordinate(2, 0);
        // foreach (var neighbor in graph.AdjacencyList[vertex])
        // {
        //     Console.WriteLine($"Reachable from 2,0 => {neighbor.X()}, {neighbor.Y()}");
        // }

        Console.WriteLine($"Starts found {starts.Count}");
        List<int> distances = new List<int>();
        int x = 0;
        foreach (Coordinate start in starts)
        {
            Console.WriteLine($"Starting in {start.X()}, {start.Y()}");

            try
            {
                var shortestPath = ShortestPathFunction(graph, start);
                distances.Add(shortestPath(finish).Count() - 1);
                // Console.WriteLine(string.Join(", ", shortestPath(finish)));
                Console.WriteLine($"Solution length {x}: {distances[x]}");
            }
            catch (Exception)
            {
                Console.WriteLine($"No valid solution");
            }
            x++;
        }

        Console.WriteLine($"Answer #2 is {distances.Min()}");
    }
    
    public static Func<Coordinate, IEnumerable<Coordinate>> ShortestPathFunction(Graph<Coordinate> graph, Coordinate start) {
        var previous = new Dictionary<Coordinate, Coordinate>();
        
        var queue = new Queue<Coordinate>();
        queue.Enqueue(start);

        while (queue.Count > 0) {
            var vertex = queue.Dequeue();

            foreach(var neighbor in graph.AdjacencyList[vertex]) {
                if (previous.ContainsKey(neighbor))
                    continue;
            
                previous[neighbor] = vertex;
                queue.Enqueue(neighbor);
            }
        }

        Func<Coordinate, IEnumerable<Coordinate>> shortestPath = v => {
            var path = new List<Coordinate>{};

            var current = v;
            while (!current.Equals(start)) {
                path.Add(current);
                if (previous.ContainsKey(current) == false)
                {
                    throw new Exception($"Did not reach {current.X()}, {current.Y()}");
                }
                current = previous[current];
            };

            path.Add(start);
            path.Reverse();

            return path;
        };

        return shortestPath;
    }

    static int GetHeight(char letter)
    {
        return Convert.ToInt32(letter) - Convert.ToInt32('a');
    }
}