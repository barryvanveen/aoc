namespace aoc2022day12;

public class MyNode
{
    private int[,] _heights;
    private int[,] _minDistances;
    private int _height;
    private int _cols;
    private int _rows;
    private Coordinate _position;
    private List<Coordinate> _visited;
    private List<MyNode> _neighbors = new ();

    public MyNode(
        Coordinate position,
        int[,] heights,
        List<Coordinate> visited,
        int[,] minDistances
    )
    {
        _position = position;
        _minDistances = minDistances;
        
        _heights = heights;
        _height = _heights[position.X(), position.Y()];
        _cols = _heights.GetLength(0);
        _rows = _heights.GetLength(1);
        
        _visited = new List<Coordinate>();
        foreach (Coordinate x in visited)
        {
            _visited.Add(x);
        }
        _visited.Add(position);
    }

    public int DepthFirstSearch(Coordinate finish)
    {
        if (_minDistances[_position.X(), _position.Y()] != -1)
        {
            return _minDistances[_position.X(), _position.Y()];
        }
        
        if (_position.Equals(finish))
        {
            Console.WriteLine($"Solution found in {_visited.Count} steps!");
             // = _visited.Count;
            // _visited.Reverse();
            foreach (Coordinate path in _visited)
            {
                Console.Write($"{path.X()},{path.Y()} => ");
            }
            Console.WriteLine();
            return _minDistances[_position.X(), _position.Y()] = 0;
        }

        // Console.WriteLine($"Traverse neighbor of {_position.X()},{_position.Y()}");
        //
        // foreach (Coordinate path in _visited)
        // {
        //     Console.Write($"{path.X()},{path.Y()} => ");
        // }
        // Console.WriteLine();
        
        AddNeighbors();

        return TraverseNeighbors(finish);
    }

    public void AddNeighbors()
    {
        List<Coordinate> options = new List<Coordinate>();
        
        options.Add(new Coordinate(_position.X(), _position.Y()-1)); // up
        options.Add(new Coordinate(_position.X()+1, _position.Y())); // right
        options.Add(new Coordinate(_position.X(), _position.Y()+1)); // down
        options.Add(new Coordinate(_position.X()-1, _position.Y())); // left

        foreach (Coordinate option in options)
        {
            if (
                option.X() >= 0 &&
                option.Y() >= 0 &&
                option.X() < _cols &&
                option.Y() < _rows &&
                NotVisited(option) &&
                Reachable(option)
            )
            {
                _neighbors.Add(new MyNode(option, _heights, _visited, _minDistances));
                // Console.WriteLine($"Add neighbor {option.X()},{option.Y()}");
            }
        }
    }

    public int TraverseNeighbors(Coordinate finish)
    {
        if (_neighbors.Count == 0)
        {
            return _minDistances[_position.X(), _position.Y()] = int.MaxValue;
        }
        
        int[] distance = Enumerable.Repeat(-1, _neighbors.Count).ToArray();
        
        for(int x = 0; x < _neighbors.Count; x++)
        {
            distance[x] = _neighbors[x].DepthFirstSearch(finish);
        }

        int min = distance.Min();

        if (min == int.MaxValue)
        {
            return _minDistances[_position.X(), _position.Y()] = int.MaxValue;
        }
        
        return _minDistances[_position.X(), _position.Y()] = min + 1;

        // if (min == -1)
        // {
        //     Console.WriteLine($"No viable children");
        //     Console.WriteLine($"Currently in {_position.X()},{_position.Y()}");
        //     
        //     foreach (Coordinate path in _visited)
        //     {
        //         Console.Write($"{path.X()},{path.Y()} => ");
        //     }
        //     Console.WriteLine();
        //     throw new Exception("Dead end!");
        // }
    }

    private bool Reachable(Coordinate neighbor)
    {
        return _heights[neighbor.X(), neighbor.Y()] <= _height + 1;
    }

    private bool NotVisited(Coordinate neighbor)
    {
        return _visited.Contains(neighbor) == false;
    }
}