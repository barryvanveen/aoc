// See https://aka.ms/new-console-template for more information

using aoc2022day11;

class Program
{
    static void Main(string[] args)
    {
        // PartOne();
        PartTwo();
    }

    // public static void PartOne()
    // {
    //     var relief = (Int64 x) => x / 3;
    //     int rounds = 20;
    //     
    //     Monkey[] monkeys = new[]
    //     {
    //         new Monkey(new List<Int64> { 79, 98 }, (Int64 x) => x * 19, relief, 23, 2, 3),
    //         new Monkey(new List<Int64> { 54, 65, 75, 74 }, (Int64 x) => x + 6, relief, 19, 2, 0),
    //         new Monkey(new List<Int64> { 79, 60, 97 }, (Int64 x) => x * x,  relief, 13, 1, 3),
    //         new Monkey(new List<Int64> { 74 }, (Int64 x) => x + 3,  relief , 17, 0, 1),
    //         
    //         // new Monkey(new List<Int64> { 65, 58, 93, 57, 66 }, (x) => x * 7, relief, 19, 6, 4),
    //         // new Monkey(new List<Int64> { 76, 97, 58, 72, 57, 92, 82 }, (x) => x + 4, relief, 3, 7, 5),
    //         // new Monkey(new List<Int64> { 90, 89, 96 }, (x) => x * 5, relief, 13, 5, 1),
    //         // new Monkey(new List<Int64> { 72, 63, 72, 99 }, (x) => x * x, relief, 17, 0, 4),
    //         // new Monkey(new List<Int64> { 65 }, (x) => x + 1, relief, 2, 6, 2),
    //         // new Monkey(new List<Int64> { 97, 71 }, (x) => x + 8, relief, 11, 7, 3),
    //         // new Monkey(new List<Int64> { 83, 68, 88, 55, 87, 67 }, (x) => x + 2, relief, 5, 2, 1),
    //         // new Monkey(new List<Int64> { 64, 81, 50, 96, 82, 53, 62, 92 }, (x) => x + 5, relief, 7, 3, 0),
    //     };
    //
    //     for (int round = 0; round < rounds; round++)
    //     {
    //         foreach (Monkey monkey in monkeys)
    //         {
    //             try
    //             {
    //                 while (true)
    //                 {
    //                     MonkeyThrow result = monkey.InspectAndThrow();
    //                     Console.WriteLine($"Monkey throws {result.Item} to monkey {result.ToMonkey}");
    //                     monkeys[result.ToMonkey].CatchItem(result.Item); 
    //                 }
    //             }
    //             catch (InvalidOperationException)
    //             {
    //                 Console.WriteLine();
    //             }
    //         }
    //     }
    //
    //     List<Int64> inspections = new List<Int64>();
    //     foreach (Monkey monkey in monkeys)
    //     {
    //         inspections.Add(monkey.Inspections());
    //         Console.WriteLine($"Monkey inspected {monkey.Inspections()} items");
    //     }
    //     inspections = inspections.OrderByDescending(x => x).ToList();
    //     
    //     Console.WriteLine($"Answer #1 is {inspections[0]}*{inspections[1]} = {inspections[0]*inspections[1]}");
    // }
    
    public static void PartTwo()
    {
        var relief = (Int64 x) => x % 9699690;
        int rounds = 10000;
        
        var watch = System.Diagnostics.Stopwatch.StartNew();

        Monkey[] monkeys = {
            // new Monkey(new List<Int64> { 79, 98 }, (x) => checked(x * 19), relief, 
            //     23, 2, 3),
            // new Monkey(new List<Int64> { 54, 65, 75, 74 }, (x) => checked(x + 6), relief, 
            //     19, 2, 0),
            // new Monkey(new List<Int64> { 79, 60, 97 }, (x) => checked(x * x),  relief, 
            //     13, 1, 3),
            // new Monkey(new List<Int64> { 74 }, (x) => checked(x + 3),  relief, 
            //     17, 0, 1),
            // 96577
            
            new Monkey(new List<Int64> { 65, 58, 93, 57, 66 }, (x) => x * 7, relief, 
                19, 6, 4),
            new Monkey(new List<Int64> { 76, 97, 58, 72, 57, 92, 82 }, (x) => x + 4, relief, 
                3, 7, 5),
            new Monkey(new List<Int64> { 90, 89, 96 }, (x) => x * 5, relief, 
                13, 5, 1),
            new Monkey(new List<Int64> { 72, 63, 72, 99 }, (x) => x * x, relief, 
                17, 0, 4),
            new Monkey(new List<Int64> { 65 }, (x) => x + 1, relief, 2, 
                6, 2),
            new Monkey(new List<Int64> { 97, 71 }, (x) => x + 8, relief, 11, 
                7, 3),
            new Monkey(new List<Int64> { 83, 68, 88, 55, 87, 67 }, (x) => x + 2, relief, 
                5, 2, 1),
            new Monkey(new List<Int64> { 64, 81, 50, 96, 82, 53, 62, 92 }, (x) => x + 5, relief, 
                7, 3, 0),
            
            // 9699690
        };
    
        for (int round = 0; round < rounds; round++)
        {
            foreach (Monkey monkey in monkeys)
            {
                try
                {
                    while (true)
                    {
                        MonkeyThrow result = monkey.InspectAndThrow();
                        if (round == rounds - 1)
                        {
                            // Console.WriteLine($"Monkey throws {result.Item} to monkey {result.ToMonkey}");
                        }
                        monkeys[result.ToMonkey].CatchItem(result.Item); 
                    }
                }
                catch (InvalidOperationException)
                {
                    // Console.WriteLine($"Monkey {monkey} is out of items\n");
                }
            }
        }
    
        List<Int64> inspections = new List<Int64>();
        foreach (Monkey monkey in monkeys)
        {
            inspections.Add(monkey.Inspections());
            Console.WriteLine($"Monkey inspected {monkey.Inspections()} items");
        }
        inspections = inspections.OrderByDescending(x => x).ToList();

        Console.WriteLine($"Answer #2 is {inspections[0]}*{inspections[1]} = {inspections[0]*inspections[1]}");
        
        // the code that you want to measure comes here
        watch.Stop();
        var elapsedMs = watch.ElapsedMilliseconds;
        Console.WriteLine($"Script took {elapsedMs/1000} seconds to run");

        if (elapsedMs > 1000 * 60)
        {
            Console.WriteLine($"That is {elapsedMs/1000/60} minutes");
        }
        
        if (elapsedMs > 1000 * 60 * 60)
        {
            Console.WriteLine($"That is {elapsedMs/1000/60/60} hours");
        }
    }
}

// Without overflow protection
// Monkey inspected 5029 items
//     Monkey inspected 4967 items
//     Monkey inspected 462 items
//     Monkey inspected 5245 items
