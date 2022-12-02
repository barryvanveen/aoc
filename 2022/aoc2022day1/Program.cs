// See https://aka.ms/new-console-template for more information

class Program
{
    static void Main(string[] args)
    {
        string filename = args[0];
        string[] lines = File.ReadAllLines(filename);

        Dictionary<int, int> calories = new Dictionary<int, int>();
        int elf = 0;
        
        Console.WriteLine(lines.Length);

        foreach (string line in lines)
        {
            if (line == "")
            {
                elf++;
                continue;
            }

            if (!calories.ContainsKey(elf))
            {
                calories[elf] = 0;
            }
            
            calories[elf] += Convert.ToInt32(line);
        }

        // foreach (KeyValuePair<int, int> item in calories)
        // {
        //     Console.WriteLine("Elf {0} caries {1} calories", item.Key, item.Value);
        // }

        var mostCaloriesValue = calories.Values.Max();
        Console.WriteLine("Answer #1 is {0}", mostCaloriesValue);

        IOrderedEnumerable<KeyValuePair<int, int>> query = 
            calories.OrderByDescending(x => x.Value);

        int top = 3;
        int sum = 0;
        foreach (KeyValuePair<int,int> item in query)
        {
            if (top == 0)
            {
                break;
            }
            
            Console.WriteLine("Elf {0} caries {1} calories", item.Key+1, item.Value);
            sum += item.Value;
            top--;
        }

        Console.WriteLine("Top 3 elves are carrying {1} calories", top, sum);
        Console.WriteLine("Answer #2 is {0}", sum);
    }
}

