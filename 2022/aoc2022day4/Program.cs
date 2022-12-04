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
        int containing = 0;
        
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            
            // Console.WriteLine($"Section {parts[0]} and {parts[1]}");
            
            string[] sections1 = parts[0].Split('-');
            string[] sections2 = parts[1].Split('-');

            Section elf1 = new Section(sections1[0], sections1[1]);
            Section elf2 = new Section(sections2[0], sections2[1]);

            Console.WriteLine($"Elf 1: start {sections1[0]}, end {sections1[1]}");
            Console.WriteLine($"Elf 2: start {sections2[0]}, end {sections2[1]}");

            if (elf1.Contains(elf2) || elf2.Contains(elf1))
            {
                containing++;
            }
        }
        
        Console.WriteLine($"Answer #1 is {containing}");
    }
    
    private static void PartTwo(string[] lines)
    {
        int overlapping = 0;
        
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            
            // Console.WriteLine($"Section {parts[0]} and {parts[1]}");
            
            string[] sections1 = parts[0].Split('-');
            string[] sections2 = parts[1].Split('-');

            Section elf1 = new Section(sections1[0], sections1[1]);
            Section elf2 = new Section(sections2[0], sections2[1]);

            Console.WriteLine($"Elf 1: start {sections1[0]}, end {sections1[1]}");
            Console.WriteLine($"Elf 2: start {sections2[0]}, end {sections2[1]}");

            if (elf1.Overlaps(elf2) || elf2.Overlaps(elf1))
            {
                overlapping++;
            }
        }
        
        Console.WriteLine($"Answer #2 is {overlapping}");
    }
}