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
        int length = lines.Length;
        List<int> input = new List<int>();
        List<int> processed = new List<int>();

        for (int x = 0; x < length; x++)
        {
            input.Add(Convert.ToInt32(lines[x]));
            processed.Add(0);
        }

        // Console.WriteLine($"Read {length} lines");
        PrintList(input);
        PrintList(processed);

        for (int x = 0; x < length; x++)
        {
            // search first unprocessed input
            var target = processed.FindIndex(x => x == 0);

            if (target == -1)
            {
                Console.WriteLine($"No more input to process");
                break;
            }
            
            // Console.WriteLine($"Processing {target}");

            // move left or right
            var direction = "right";
            if (input[target] < 0)
            {
                direction = "left";
            }
            
            // move
            int newPosition = Modulo(target + input[target], length-1);

            // Console.WriteLine($"{target}: {direction} {input[target]} => {newPosition}");

            // move input
            var temp = input[target];
            input.RemoveAt(target);
            input.Insert(newPosition, temp);
            
            // set input as processed
            processed.RemoveAt(target);
            processed.Insert(newPosition, 1);
            
            // PrintList(input);
            // PrintList(processed);
            // Console.WriteLine();
        }
        
        var zero = input.FindIndex(x => x == 0);
        Console.WriteLine($"Value 0 found at {zero}");

        var thousand = (zero + 1000) % length;
        var twothousand = (zero + 2000) % length;
        var threethousand = (zero + 3000) % length;
        Console.WriteLine($"Value at 1000th after {zero} is {thousand}: {input[thousand]}");
        Console.WriteLine($"Value at 2000th after {zero} is {twothousand}: {input[twothousand]}");
        Console.WriteLine($"Value at 3000th after {zero} is {threethousand}: {input[threethousand]}");

        Console.WriteLine($"Answer #1 is {input[thousand] + input[twothousand] + input[threethousand]}");
    }
    
    static void PartTwo(string[] lines)
    {
        int length = lines.Length;
        List<Int64> input = new List<Int64>();
        List<int> order = new List<int>();
        List<Int64> modulos = new List<Int64>();

        Int64 decryptionKey = 811589153;
        for (int x = 0; x < length; x++)
        {
            input.Add(Convert.ToInt64(lines[x]) * decryptionKey);
            order.Add(x);
            modulos.Add(Modulo64(input[x], length-1));
        }

        // Console.WriteLine($"Read {length} lines");
        PrintList64(input);

        int rounds = 10;
        for (int round = 0; round < rounds; round++)
        {
            for (int x = 0; x < length; x++)
            {
                // search next index to process
                var target = order.FindIndex(item => item == x);

                if (target == -1)
                {
                    Console.WriteLine($"No more input to process");
                    break;
                }
                
                // Console.WriteLine($"Processing {x} at index {target}");

                // move left or right
                var direction = "right";
                if (input[target] < 0)
                {
                    direction = "left";
                }
                
                // move
                Int64 newPosition = target + modulos[x];
                if (newPosition >= length)
                {
                    newPosition = Modulo64(newPosition, length - 1);
                }

                // Console.WriteLine($"{target}: {direction} {target} + {modulos[x]} => {newPosition}");

                // move input
                var temp = input[target];
                input.RemoveAt(target);
                input.Insert(Convert.ToInt32(newPosition), temp);

                // reshuffle order
                order.RemoveAt(target);
                order.Insert(Convert.ToInt32(newPosition), x);
                
                // PrintList64(input);
                // Console.WriteLine();
            }

            // Console.WriteLine($"After round {round + 1}");
            // PrintList64(input);
            // Console.WriteLine();
        }

        var zero = input.FindIndex(x => x == 0);
        Console.WriteLine($"Value 0 found at {zero}");

        var thousand = (zero + 1000) % length;
        var twothousand = (zero + 2000) % length;
        var threethousand = (zero + 3000) % length;
        Console.WriteLine($"Value at 1000th after {zero} is {thousand}: {input[thousand]}");
        Console.WriteLine($"Value at 2000th after {zero} is {twothousand}: {input[twothousand]}");
        Console.WriteLine($"Value at 3000th after {zero} is {threethousand}: {input[threethousand]}");

        Console.WriteLine($"Answer #1 is {input[thousand] + input[twothousand] + input[threethousand]}");
    }
    
    private static int Modulo(int x, int m) 
    {
        return (x%m + m)%m;
    }

    private static Int64 Modulo64(Int64 x, Int64 m) 
    {
        return (x%m + m)%m;
    }
    
    private static void PrintList(List<int> list)
    {
        for (int y = 0; y < list.Count; y++)
        {
            Console.Write($"{list[y]}, ");
        }
        
        Console.Write($"\n");
    }
    
    private static void PrintList64(List<Int64> list)
    {
        for (int y = 0; y < list.Count; y++)
        {
            Console.Write($"{list[y]}, ");
        }
        
        Console.Write($"\n");
    }
}