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
    
    static void PartOne(string[] lines) 
    {
        List<Tuple<ItemList, ItemList>> input = new List<Tuple<ItemList, ItemList>>();
        
        for (int x = 0; x < lines.Length; x++)
        {
            //Console.WriteLine($" -- {lines[x]}");
            var left = new ItemList(lines[x]);  
            // left[left.Count - 1].Debug();
            
            //Console.WriteLine($" -- {lines[x+1]}");
            var right = new ItemList(lines[x+1]);
            // right[right.Count - 1].Debug();
            
            input.Add(new Tuple<ItemList, ItemList>(left, right));

            x += 2;
        }

        int index = 0;
        List<int> correctIndices = new List<int>();
        foreach (Tuple<ItemList, ItemList> row in input)
        {
            var left = row.Item1;
            var right = row.Item2;

            var correct = left.CompareTo(right);
            Console.WriteLine(correct);
            if (correct == 1)
            {
                correctIndices.Add(index+1);
                Console.WriteLine($"Order is correct");
            }
            else
            {
                Console.WriteLine($"Order is NOT correct");
            }

            Console.WriteLine();
            
            index++;
        }

        Console.Write($"Correct indices: ");
        foreach (int correctIndex in correctIndices)
        {
            Console.Write(correctIndex);
        }
        Console.WriteLine();
        
        Console.WriteLine($"Answer #1 is: {correctIndices.Sum()}");
    }

    static void PartTwo(string[] lines)
    {
        List<ItemList> input = new List<ItemList>();
        
        for (int x = 0; x < lines.Length; x++)
        {
            if (lines[x] == "")
            {
                continue;
            }
            
            input.Add(new ItemList(lines[x]));
        }
        
        input.Add(new ItemList("[[2]]"));
        input.Add(new ItemList("[[6]]"));
        
        foreach (var item in input)
        {
            item.Debug();
            Console.WriteLine();
        }
        
        // sort input
        input.Sort();
        input.Reverse();

        Console.WriteLine($"\n\nSorted list:");
        var index = 1;
        foreach (var item in input)
        {
            Console.Write($"{index} --> ");
            item.Debug();
            Console.WriteLine();
            index++;
        }
    }
}

enum Types
{
    Value,
    List,
}

interface IItem: IComparable<IItem>
{
    public void Debug();

    public Types Type();

    public int Value();
}

class ItemValue: IItem
{
    private int _value;

    public ItemValue(string line)
    {
        _value = Convert.ToInt32(line);
    }

    public void Debug()
    {
        Console.Write($"{_value } ");
    }

    public Types Type()
    {
        return Types.Value;
    }

    public int Value()
    {
        return _value;
    }

    public int CompareTo(IItem? other)
    {
        Console.Write("Compare ");
        Debug();
        Console.Write(" to ");
        other.Debug();
        Console.Write("\n");
        
        if (other.Type() == Types.Value)
        {
            if (_value < other.Value())
            {
                return 1;
            }
            
            if (_value > other.Value())
            {
                return -1;
            } 
            
            return 0;
        }

        if (other.Type() == Types.List)
        {
            var otherList = other as ItemList;
            Console.WriteLine($"Mixed types; convert left {Value()} to list");
            ItemList thisAsList = new ItemList(this);
            return thisAsList.CompareTo(otherList);    
        }

        throw new Exception("Unknown type of {other}");
    }
}

class ItemList: IItem
{
    private List<IItem> _queue = new List<IItem>();

    public ItemList(string line)
    {
        for (int x = 0; x < line.Length; x++)
        {
            // start of list
            if (line[x] == '[')
            {
                // find end of list
                int depth = 0;
                for (int y = x + 1; y < line.Length; y++)
                {
                    if (line[y] == '[')
                    {
                        depth++;
                        continue;
                    }

                    if (line[y] == ']')
                    {
                        if (depth >= 1)
                        {
                            depth--;
                            continue;
                        }

                        // end of list found
                        // Console.WriteLine($"Found list: {line.Substring(x+1, y-x-1)}");
                        _queue.Add(new ItemList(line.Substring(x+1, y-x-1)));
                        x = y + 1;
                        break;
                    }
                }

                continue;
            }

            // start of value
            if (Char.IsDigit(line[x]))
            {
                // Console.WriteLine($"Start of value: {line[x]}");
                // find end of value
                int y;
                for (y = x + 1; y < line.Length; y++)
                {
                    if (line[y] == ',' || line[y] == '[' || line[y] == ']')
                    {
                        break;
                    }
                }
                // end of value found
                // Console.WriteLine($"Found value: {line.Substring(x, y-x)}");
                _queue.Add(new ItemValue(line.Substring(x, y-x)));
                x = y;
                continue;
            }

            // found comma
            if (line[x] == ',')
            {
                continue;
            }
        }
    }

    public ItemList(ItemValue value)
    {
        _queue.Add(value);
    }

    public void Debug()
    {
        Console.Write("[ ");
        foreach (var item in _queue)
        {
            item.Debug();
        }
        Console.Write("] ");
    }
    
    public Types Type()
    {
        return Types.List;
    }

    public int Value()
    {
        throw new Exception("Should not call Value() on list!");
    }

    public IItem Get(int index)
    {
        return _queue[index];
    }

    public int Count()
    {
        return _queue.Count;
    }
    
    public int CompareTo(IItem? other)
    {
        Console.Write("Compare ");
        Debug();
        Console.Write(" to ");
        other.Debug();
        Console.Write("\n");

        if (other.Type() == Types.Value)
        {
            Console.WriteLine($"Mixed types; convert right {other.Value()} to list");
            return CompareTo(new ItemList(other as ItemValue));
        }
        
        if (other.Type() == Types.List)
        {
            var otherList = other as ItemList;
            var leftCount = Count();
            var rightCount = otherList.Count();

            for (int x = 0; x < Math.Min(leftCount, rightCount); x++)
            {
                var left = Get(x);
                var right = otherList.Get(x);
                
                var correct = left.CompareTo(right);
                if (correct == -1)
                {
                    return -1;
                }

                if (correct == 1)
                {
                    return 1;
                }
            }

            if (otherList.Count() < Count())
            {
                Console.WriteLine($"Right list runs out of items");
                return -1;   
            }
            
            if (otherList.Count() > Count())
            {
                Console.WriteLine($"Left list runs out of items");
                return 1;
            }
            
            return 0;
        }
        
        throw new Exception("Unknown type of {other}");
    }
}