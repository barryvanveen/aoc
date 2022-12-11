// See https://aka.ms/new-console-template for more information

using aoc2022day9;

class Program
{
    static void Main(string[] args)
    {
        string filename = args[0];
        string[] lines = File.ReadAllLines(filename);
        
        List<Step> steps = new List<Step>();
        foreach (string line in lines)
        {
            steps.Add(new Step(line.Split(" ")[0], line.Split(" ")[1]));
        }

        // PartOne(steps);
        PartTwo(steps);
    }

    public static void PartOne(List<Step> steps)
    {
        Coordinate head = new Coordinate(0, 0);
        Coordinate tail = new Coordinate(0, 0);
        List<Coordinate> tailCoordinates = new List<Coordinate>();

        Console.WriteLine($"Current position of head: {head.Debug()}");
        foreach (Step step in steps)
        {
            step.Debug();
            
            for (int x = 0; x < step.GetSize(); x++)
            {
                head = head.Move(step.GetDirection());
                Console.WriteLine($"Current position of head: {head.Debug()}");
                tail = tail.Follow(head);
                tailCoordinates.Add(tail);
                Console.WriteLine($"Current position of tail: {tail.Debug()}");
            }
        }

        string[] uniqueTailCoordinates = tailCoordinates.Select(x => x.ToString()).ToArray();

        Console.WriteLine($"Answer #1 is {uniqueTailCoordinates.Distinct().ToArray().Length}");
    }
    
    public static void PartTwo(List<Step> steps)
    {
        int knots = 10;
        Coordinate[] rope = new Coordinate[knots];
        List<Coordinate> tailCoordinates = new List<Coordinate>();

        for (int knot = 0; knot < knots; knot++)
        {
            rope[knot] = new Coordinate(0, 0);
        }

        PrintGrid(rope);
        foreach (Step step in steps)
        {
            step.Debug();
            
            for (int x = 0; x < step.GetSize(); x++)
            {
                rope[0] = rope[0].Move(step.GetDirection());
                // Console.WriteLine($"Head: {rope[0].Debug()}");

                for (int knot = 1; knot < knots; knot++)
                {
                    rope[knot] = rope[knot].Follow(rope[knot-1]);
                    // Console.WriteLine($"  {knot}: {rope[knot].Debug()}");

                    if (knot == knots - 1)
                    { 
                        tailCoordinates.Add(rope[knot]);
                    }
                }
                
                // PrintGrid(rope);
            }
            
            // PrintGrid(rope);
        }
        
        string[] uniqueTailCoordinates = tailCoordinates.Select(x => x.ToString()).ToArray();
        int sum = uniqueTailCoordinates.Distinct().ToArray().Length;

        Console.WriteLine($"Answer #2 is {sum}");
    }

    public static void PrintGrid(Coordinate[] rope)
    {
        for (int y = 15; y > -15; y--)
        {
            for (int x = -15; x < 15; x++)
            {
                bool found = false;
                for (int k = 0; k < rope.Length; k++)
                {
                    if (rope[k].X() == x && rope[k].Y() == y)
                    {
                        if (k == 0)
                        {
                            Console.Write('H');
                        }
                        else
                        {
                            Console.Write(k);
                        }
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    if (x == 0 && y == 0)
                    {
                        Console.Write("s");
                    } else
                    {
                        Console.Write(".");
                    }
                }
            }

            Console.Write("\n");
        }
    }
}