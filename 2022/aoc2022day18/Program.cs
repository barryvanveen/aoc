// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

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
        List<string> index = new List<string>();
        List<Cube> cubes = new List<Cube>();
        
        foreach (string line in lines)
        {
            var parts = line.Split(',');
            var cube = new Cube(parts[0], parts[1], parts[2]);
            cubes.Add(cube);
            index.Add(cube.ToString());
        }

        int sides = 0;
        foreach (Cube x in cubes)
        {
            foreach (Cube neighbor in Neighbors(x))
            {
                if (!index.Contains(neighbor.ToString()))
                {
                    sides++;
                }
            }
        }
        
        Console.WriteLine($"Answer #1 is {sides}");
    }
    
    static void PartTwo(string[] lines)
    {
        List<string> index = new List<string>();
        List<Cube> cubes = new List<Cube>();
        List<int> xs = new List<int>();
        List<int> ys = new List<int>();
        List<int> zs = new List<int>();

        foreach (string line in lines)
        {
            var parts = line.Split(',');
            var cube = new Cube(parts[0], parts[1], parts[2]);
            cubes.Add(cube);
            index.Add(cube.ToString());
            
            xs.Add(cube.X);
            ys.Add(cube.Y);
            zs.Add(cube.Z);
        }
        
        Console.WriteLine($"Grid runs from {xs.Min()},{ys.Min()},{zs.Min()} to {xs.Max()},{ys.Max()},{zs.Max()}");

		// Floodfill to find cubes on the outside of the droplet
		List<string> outside = new List<string>();
		Queue<Cube> queue = new Queue<Cube>();
		queue.Enqueue(new Cube(xs.Min()-1, ys.Min()-1, zs.Min()-1));	
		while(queue.Count > 0)
		{
			var current = queue.Dequeue();

			if (current.X < xs.Min()-1 || current.X > xs.Max()+1 || current.Y < ys.Min()-1 || current.Y > ys.Max()+1 || current.Z < zs.Min()-1 || current.Z > zs.Max()+1)
			{
				continue;
			}

			// is not outside?
			if (index.Contains(current.ToString())) 
			{
				continue;
			}

			if (outside.Contains(current.ToString()))
			{
				continue;
			}

			// mark as outside
			outside.Add(current.ToString());

			// enqueue neighbors
			foreach (Cube neighbor in Neighbors(current))
			{
				queue.Enqueue(neighbor);
			}
		}

		// Count cube sides that touch an outside cube
		int sides = 0;
        foreach (Cube x in cubes)
        {
            foreach (Cube neighbor in Neighbors(x))
            {
                if (outside.Contains(neighbor.ToString()))
                {
                    sides++;
                }
            }
        }

        Console.WriteLine($"Answer #2 is {sides}");
    }

    private static Cube[] Neighbors(Cube c)
    {
        return new Cube[]
        {
            new Cube(c.X-1, c.Y, c.Z),
            new Cube(c.X+1, c.Y, c.Z),
            new Cube(c.X, c.Y-1, c.Z),
            new Cube(c.X, c.Y+1, c.Z),
            new Cube(c.X, c.Y, c.Z-1),
            new Cube(c.X, c.Y, c.Z+1),
        };
    }
}

class Cube: IEquatable<Cube>
{
    public readonly int X;
    public readonly int Y;
    public readonly int Z;

    public Cube(string x, string y, string z)
    {
        X = Convert.ToInt32(x);
        Y = Convert.ToInt32(y);
        Z = Convert.ToInt32(z);
    }
    
    public Cube(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public override string ToString()
    {
        return $"{X},{Y},{Z}";
    }

    public bool Equals(Cube? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Cube)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }
}