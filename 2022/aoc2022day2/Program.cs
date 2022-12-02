// See https://aka.ms/new-console-template for more information

using aoc2022day2;

class Program
{
    static void Main(string[] args)
    {
        string filename = args[0];
        string[] lines = File.ReadAllLines(filename);

        PartOne(lines);
        PartTwo(lines);
    }

    private static void PartOne(string[] lines)
    {
        int sum = 0;
        
        foreach (string x in lines)
        {
            var parts = x.Split(' ');
            var line = new RockPaperScissorsLine(parts[0], parts[1]);
            var score = line.Score();
            
            Console.WriteLine($"Total round score {score}");
            
            sum += score;
        }
        
        Console.WriteLine($"Answer #1 is {sum}");
    }
    
    private static void PartTwo(string[] lines)
    {
        int sum = 0;
        
        foreach (string x in lines)
        {
            var parts = x.Split(' ');

            var decider = new RockPaperScissorsShapeDecider(parts[0], parts[1]);
            
            var opponentShape = parts[0];
            var myShape = decider.Decide();
            
            var line = new RockPaperScissorsLine(opponentShape, myShape);
            var score = line.Score();
            
            Console.WriteLine($"Total round score {score}");
            
            sum += score;
        }
        
        Console.WriteLine($"Answer #2 is {sum}");
    }
}