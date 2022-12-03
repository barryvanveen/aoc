// See https://aka.ms/new-console-template for more information

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
            // Console.WriteLine(x);
            // Console.WriteLine($"Length {x.Length}");
            
            var compartment1 = x.Substring(0, x.Length / 2);
            var compartment2 = x.Substring(x.Length/2);
            
            // Console.WriteLine(compartment1);
            // Console.WriteLine(compartment2);
            
            // find common denominator
            char common = GetCommonItem(compartment1, compartment2);
            Console.WriteLine($"Common item is {common}");

            int value = GetItemValue(common);
            Console.WriteLine($"Value of {common} is {value}");
            sum += value;
        }

        Console.WriteLine($"Answer #1 is {sum}");
    }

    private static char GetCommonItem(string compartment1, string compartment2)
    {
        foreach (char letter1 in compartment1)
        {
            foreach (char letter2 in compartment2)
            {
                if (letter1 == letter2)
                {
                    return letter1;
                }
            }
        }

        throw new Exception($"Could not find common char in \"{compartment1}\" and \"{compartment2}\"");
    }

    private static int GetItemValue(char item)
    {
        var value = (int)item;
        
        if (value >= 97)
        {
            return value - 96;
        }

        return value - 38;
    }

    private static void PartTwo(string[] lines)
    {
        int sum = 0;
        
        for (int elf = 0; elf < lines.Length-2; elf++)
        {
            char common = GetCommonItem(lines[elf], lines[elf + 1], lines[elf + 2]);
            Console.WriteLine($"Common item is {common}");
            
            int value = GetItemValue(common);
            Console.WriteLine($"Value of {common} is {value}");
            sum += value;
            
            elf += 2;
        }
        
        Console.WriteLine($"Answer #2 is {sum}");
    }

    private static char GetCommonItem(string rucksack1, string rucksack2, string rucksack3)
    {
        foreach (char letter1 in rucksack1)
        {
            foreach (char letter2 in rucksack2)
            {
                foreach (char letter3 in rucksack3)
                {
                    if (letter1 == letter2 && letter1 == letter3)
                    {
                        return letter1;
                    }
                }
            }
        }
        
        throw new Exception($"Could not find common char in \"{rucksack1}\" and \"{rucksack2}\" and \"{rucksack3}\"");
    }

}