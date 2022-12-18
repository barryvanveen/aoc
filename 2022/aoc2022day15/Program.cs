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
        List<Sensor> sensors = new List<Sensor>();
        List<Coordinate> beacons = new List<Coordinate>(); 
        List<int> xs = new List<int>();
        List<int> ys = new List<int>();
        
        foreach (string line in lines)
        {
            string pattern = @".*x=(-?[\d]+).*y=(-?[\d]+).*x=(-?[\d]+).*y=(-?[\d]+)";
            Regex rgx = new Regex(pattern);
            
            Match match = rgx.Match(line);

            if (!match.Success)
            {
                throw new Exception($"Could not parse input line: {line}");
            }

            var beacon = new Coordinate(match.Groups[4].Value, match.Groups[3].Value); 
            var sensor = new Sensor(
                new Coordinate(match.Groups[2].Value, match.Groups[1].Value),
                beacon
            );
            sensors.Add(sensor);
            beacons.Add(beacon);

            xs.Add(sensor.MinX);
            xs.Add(sensor.MaxX);
            ys.Add(sensor.MinY);
            ys.Add(sensor.MaxY);
        }

        var minX = xs.Min();
        var maxX = xs.Max();
        var minY = ys.Min();
        var maxY = ys.Max();
        
        Console.WriteLine($"Grid is from {minY},{minX} to {maxY},{maxX}");

        int blocked = 0;
        int y = 2000000;
        for (int x = minX; x <= maxX; x++)
        {
            var point = new Coordinate(y, x);
            
            foreach (Sensor sensor in sensors)
            {
                if (sensor.WithinRange(point))
                {
                    // Console.WriteLine($"Blocked on position {point.X}");
                    blocked++;
                    break;
                }
            }

            foreach (Coordinate beacon in beacons)
            {
                // Console.WriteLine($"Checking beacon on position {point.Y},{point.X}");
                if (point.Equals(beacon))
                {
                    // Console.WriteLine($"Beacon on position {point.X}");
                    blocked--;
                    break;
                }
            }
        }

        Console.WriteLine($"Answer #1 is {blocked}");
    }
    
    static void PartTwo(string[] lines)
    {
        List<Sensor> sensors = new List<Sensor>();
        List<Coordinate> beacons = new List<Coordinate>(); 
        List<int> xs = new List<int>();
        List<int> ys = new List<int>();
        
        foreach (string line in lines)
        {
            string pattern = @".*x=(-?[\d]+).*y=(-?[\d]+).*x=(-?[\d]+).*y=(-?[\d]+)";
            Regex rgx = new Regex(pattern);
            
            Match match = rgx.Match(line);

            if (!match.Success)
            {
                throw new Exception($"Could not parse input line: {line}");
            }

            var beacon = new Coordinate(match.Groups[4].Value, match.Groups[3].Value); 
            var sensor = new Sensor(
                new Coordinate(match.Groups[2].Value, match.Groups[1].Value),
                beacon
            );
            sensors.Add(sensor);
            beacons.Add(beacon);

            // xs.Add(sensor.MinX());
            // xs.Add(sensor.MaxX());
            // ys.Add(sensor.MinY());
            // ys.Add(sensor.MaxY());
        }

        // var minX = xs.Min();
        // var maxX = xs.Max();
        // var minY = ys.Min();
        // var maxY = ys.Max();
        //
        // Console.WriteLine($"Grid is from {minY},{minX} to {maxY},{maxX}");

        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        int limit = 4000000; // 20
        // int round = 1000;
        for (int x = 0; x <= limit; x++)
        {
            // if (x > 0 && x % round == 0)
            // {
            //     Console.WriteLine($"Checked {x} rows");
            //     break;
            // }

            var filteredXSensors = sensors.Where(sensor => sensor.MinX <= x && sensor.MaxX >= x).ToList();
            // Console.WriteLine($"{filteredSensors.Count()} of {sensors.Count} sensors left after X-axis filtering");

            for (int y = 0; y <= limit; y++)
            {
                var point = new Coordinate(y, x);
                bool blocked = false; 
                
                // var filteredXYSensors = filteredXSensors.Where(sensor => sensor.MinY <= y && sensor.MaxY >= y).ToList();
                // Console.WriteLine($"{filteredSensors.Count()} of {sensors.Count} sensors left after Y-axis filtering");

                foreach (Sensor sensor in filteredXSensors)
                {
                    if (sensor.WithinRange(point))
                    {
                        // blocked by sensor, move y as far as we can
                        // Console.WriteLine($"Skip {skipY} positions");
                        y = sensor.GetMaxYAlongX(x);
                        
                        // Console.WriteLine($"Blocked on position {point.Y},{point.X}");
                        blocked = true;
                        break;
                    }
                }

                if (!blocked)
                {
                    Console.WriteLine($"Distress signal found at {point.Y},{point.X}");
                    BigInteger first = point.X * 4000000;
                    BigInteger tuningFrequency = first + point.Y;
                    
                    Console.WriteLine($"Answer #2 is {tuningFrequency}");
                    break;
                }
            }
        }
        
        stopWatch.Stop();
        long ms = stopWatch.ElapsedMilliseconds;
        Console.WriteLine($"Took {ms} ms");
        // int rounds = limit / round;
        // Console.WriteLine($"Would take {ms * rounds} ms to complete");
        
        // original
        // 32 days 23 hours 42 minutes
        
        // filtering sensors in outer loop (for y, for x)
        // 10 days 18 hours 9 minutes

        // upon hitting sensor, skip the most steps you can
        // 2.5 seconds
    }
}

public class Coordinate: IEquatable<Coordinate>
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

    public bool Equals(Coordinate? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Y == other.Y && X == other.X;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Coordinate)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Y, X);
    }
}

public class Sensor
{
    public readonly Coordinate Position;
    public readonly int DistanceToBeacon;
    public int MinX, MaxX, MinY, MaxY;
    
    public Sensor(Coordinate position, Coordinate beacon)
    {
        Position = position;
        // Console.WriteLine($"Sensor at {position.Y},{position.X}");
        // Console.WriteLine($"Beacon at {beacon.Y},{beacon.X}");

        DistanceToBeacon = ManhattanDistanceTo(beacon);
        // Console.WriteLine($"Manhattan distance to beacon: {DistanceToBeacon}");
        
        MinX = Position.X - DistanceToBeacon;
        MaxX = Position.X + DistanceToBeacon;
        MinY = Position.Y - DistanceToBeacon;
        MaxY = Position.Y + DistanceToBeacon;
    }

    private int ManhattanDistanceTo(Coordinate point)
    {
        return Math.Abs(Position.Y - point.Y) + Math.Abs(Position.X - point.X);
    }

    public bool WithinRange(Coordinate point)
    {
        return ManhattanDistanceTo(point) <= DistanceToBeacon;
    }

    public int GetMaxYAlongX(int x)
    {
        return Position.Y + (DistanceToBeacon - Math.Abs(Position.X - x));
    }
}


