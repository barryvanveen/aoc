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

    public static void PartOne(string[] lines)
    {
        int cycle = 0;
        int x = 1;
        Dictionary<int, int> register = new Dictionary<int, int>();

        foreach (string line in lines)
        {
            if (line == "noop")
            {
                register[cycle] = x;
                Console.WriteLine($"Tick {cycle}: {x}");
                cycle++;
                continue;
            }

            if (line.StartsWith("addx "))
            {
                register[cycle] = x;
                Console.WriteLine($"Tick {cycle}: {x}");
                cycle++;

                register[cycle] = x;
                Console.WriteLine($"Tick {cycle}: {x}");
                cycle++;

                x += Convert.ToInt32(line.Split(" ")[1]);
                continue;
            }

            throw new Exception("Unsupported operator");
        }
        
        register[cycle] = x;
        Console.WriteLine($"Tick {cycle}: {x}");
        
        // signal strength
        int strength = 0;
        strength += SignalStrength(register, 20);
        strength += SignalStrength(register, 60);
        strength += SignalStrength(register, 100);
        strength += SignalStrength(register, 140);
        strength += SignalStrength(register, 180);
        strength += SignalStrength(register, 220);

        Console.WriteLine($"Answer #1 is {strength}");
    } 

    public static int SignalStrength(Dictionary<int, int> register, int cycle)
    {
        int signalStrength = cycle * register[cycle - 1];
        Console.WriteLine($"Signal {cycle}: {signalStrength}");
        return signalStrength;
    }

    public static void PartTwo(string[] lines)
    {
        int cycle = 0;
        int spriteStart = 0;
        Dictionary<int, char> screen = new Dictionary<int, char>();

        foreach (string line in lines)
        {
            PrintSpritePosition(spriteStart);
            
            if (line == "noop")
            {
                screen[cycle] = GetCycleChar(cycle, spriteStart);
                Console.WriteLine($"Tick {cycle}: {screen[cycle]}");

                cycle++;
                continue;
            }

            if (line.StartsWith("addx "))
            {
                screen[cycle] = GetCycleChar(cycle, spriteStart);
                Console.WriteLine($"Tick {cycle}: {screen[cycle]}");
                cycle++;

                screen[cycle] = GetCycleChar(cycle, spriteStart);
                Console.WriteLine($"Tick {cycle}: {screen[cycle]}");
                cycle++;

                spriteStart += Convert.ToInt32(line.Split(" ")[1]);
                continue;
            }

            throw new Exception("Unsupported operator");
        }
        Console.WriteLine("\n\nScreen:");
        foreach ( KeyValuePair<int, char> pixel in screen)
        {
            if (pixel.Key % 40 == 0)
            {
                Console.Write("\n");
            }
            Console.Write(pixel.Value);
        }
    }
    private static void PrintSpritePosition(int spriteStart)
    {
        for (int x = 0; x < 40; x++)
        {
            if (x >= spriteStart && x <= spriteStart + 2)
            {
                Console.Write(GetCycleChar(x, spriteStart));
            }
            else
            {
                Console.Write(GetCycleChar(x, spriteStart));
            }
        }
        Console.Write("\n");
    }

    private static char GetCycleChar(int cycle, int spriteStart)
    {
        cycle %= 40;
        
        if (cycle >= spriteStart && cycle <= spriteStart + 2)
        {
            return 'X';
        }
        
        return '.';
    }
}