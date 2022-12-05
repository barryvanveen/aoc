// See https://aka.ms/new-console-template for more information

using aoc2022day5;

class Program
{
    static void Main(string[] args)
    {
        string filename = args[0];
        string[] lines = File.ReadAllLines(filename);

        List<char>[] stacks = GetStacks(lines);
        List<Step> procedure = GetProcedure(lines);
        
        PartOne(stacks, procedure);
        
		// reset input
        stacks = GetStacks(lines);
        procedure = GetProcedure(lines);
        
        PartTwo(stacks, procedure);
    }

    private static void PartOne(List<char>[] stacks, List<Step> procedure)
    {
        foreach (Step step in procedure)
        {
            for (int x = 0; x < step.Amount(); x++)
            {
                int fromLength = stacks[step.From()].Count - 1;
                stacks[step.To()].Add(stacks[step.From()][fromLength]);
                stacks[step.From()].RemoveAt(fromLength);
                Console.WriteLine($"Move {step.From()} to {step.To()}");
            }
        }

        foreach (List<char> stack in stacks)
        {
            Console.Write($"Stack: ");
            foreach (char crate in stack)
            {
                Console.Write($"{crate} ");
            }
            Console.Write("\n");
        }
        
        Console.Write("Answer #1 is ");
        foreach (List<char> stack in stacks)
        {
            Console.Write(stack[stack.Count-1]);
        }
        Console.Write("\n");
    }

    private static void PartTwo(List<char>[] stacks, List<Step> procedure)
    {
        List<char> tempStack = new List<char>();
        
        foreach (Step step in procedure)
        {
            for (int x = 0; x < step.Amount(); x++)
            {
                int fromLength = stacks[step.From()].Count - 1;
                tempStack.Add(stacks[step.From()][fromLength]);
                stacks[step.From()].RemoveAt(fromLength);
            }
            
            for (int x = 0; x < step.Amount(); x++)
            {
                int fromLength = tempStack.Count - 1;
                stacks[step.To()].Add(tempStack[fromLength]);
                tempStack.RemoveAt(fromLength);
            }
            
            Console.WriteLine($"Move {step.From()} to {step.To()}");
        }

        foreach (List<char> stack in stacks)
        {
            Console.Write($"Stack: ");
            foreach (char crate in stack)
            {
                Console.Write($"{crate} ");
            }
            Console.Write("\n");
        }
        
        Console.Write("Answer #2 is ");
        foreach (List<char> stack in stacks)
        {
            Console.Write(stack[stack.Count-1]);
        }
        Console.Write("\n");
    }

    private static List<char>[] GetStacks(string[] lines)
    {
        int columns = (lines[0].Length / 4) + 1;
        List<char>[] stacks = new List<char>[columns];

        for (int x=0; x<columns; x++)
        {
            stacks[x] = new List<char>();
        }

        foreach (string line in lines)
        {
            if (line == "" || line.Contains('[') == false)
            { 
                break;
            }

            for (int x=0; 1+(x*4) < line.Length; x++)
            {
                char crate = line[1+(x*4)];
                if (crate != ' ')
                {
                    stacks[x].Add(crate);
                }
            }
        }
        
        for (int x=0; x<columns; x++)
        {
            stacks[x].Reverse();
            foreach (char crate in stacks[x])
            {
                Console.Write($"{crate} ");
            }

            Console.WriteLine();
        }
        

        return stacks;
    }

    private static List<Step> GetProcedure(string[] lines)
    {
        bool startFound = false;
        List<Step> procedure = new List<Step>();

        foreach (string line in lines)
        {
            if (startFound == false && line != "")
            {
                continue;
            }

            if (startFound == false && line == "")
            {
                startFound = true;
                continue;
            }

            string[] parts = line.Split(" ");
            procedure.Add(new Step(parts[1], parts[3], parts[5]));
        }

        return procedure;
    }
}