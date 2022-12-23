using System.Diagnostics;
using NCalc;

class Program
{
    static Dictionary<string, string> _monkeys = new Dictionary<string, string>();

    static void Main(string[] args)
    {
        string filename = args[0];
        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            var parts = line.Split(": ");
            _monkeys.Add(parts[0], parts[1]);
        }
        
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        // PartOne();
        PartTwo();
        
        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;

        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        Console.WriteLine("RunTime " + elapsedTime);
    }

    static void PartOne()
    {
        var answer = GetMonkeyValue("root");
        
        Console.WriteLine($"Answer #1 is {answer}");
    }

    static void PartTwo()
    {
        var start = _monkeys["root"];
        Console.WriteLine($"Root: {start}");

        string[] startParts = start.Split(" ");
        int answer = -1;
        
        var expression1 = GetMonkeyExpression(startParts[0]);
        var expression2 = GetMonkeyExpression(startParts[2]); 
        
        // Console.WriteLine(expression1);
        // Console.WriteLine(expression2);

        Int64 answer2 = 37175119093215;
                      //30328634670182,613
        
        // Expression e = new Expression($"{expression1}");
        // e.Parameters["x"] = 3797498335832;
        // object expressionAnswer = e.Evaluate();
        // Console.WriteLine($"{expressionAnswer}");

        Expression e = new Expression($"{expression1}");
        Double x = 3592056845079;
        while (true)
        {
            e.Parameters["x"] = x;
        
            Double? expressionAnswer = e.Evaluate() as Double?;
            if (expressionAnswer == answer2)
            {
                Console.WriteLine($"{x}: {expressionAnswer}");
                // answer = x;
                
                //                      37175119093215
                // 10000000000000:      -176374163646547,6
                // 1000000000000:      123557264924881
                // 3797498335832,414:   30328634670168,78
                // 3452271214392,727:   41833575082947,39
                // 3624884775111,6:     36081104876590,414
                // 3588994826842,574:   37277163038333,04
                // 3592056862968,29:    37175118497274,91
                // 3592056845079,6357:  37175119093427,09
                break;
            }
        
            // x /= 1.00000000001;
            x += 1;
        }

        Console.WriteLine($"Answer #2 is {answer}");
    }
    
    static string GetMonkeyExpression(string key)
    {
        string value = _monkeys[key];

        if (key == "humn")
        {
            return "x";
        }

        if (value.Contains(' ') == false)
        {
            // Console.WriteLine($"{key}: {value}");
            return value;
        }

        // Console.WriteLine($"Split {key}: {value}");
        var parts = value.Split(" ");

        switch (parts[1])
        {
            case "+":
                return $"({GetMonkeyExpression(parts[0])}) + ({GetMonkeyExpression(parts[2])})";
            case "-":
                return $"({GetMonkeyExpression(parts[0])}) - ({GetMonkeyExpression(parts[2])})";
            case "/":
                return $"({GetMonkeyExpression(parts[0])}) / ({GetMonkeyExpression(parts[2])})";
            case "*":
                return $"({GetMonkeyExpression(parts[0])}) * ({GetMonkeyExpression(parts[2])})";
            default:
                throw new Exception("Unknown operator");
        }
    }
    
    static Int64 GetMonkeyValue(string key)
    {
        string value = _monkeys[key];

        if (value.Contains(' ') == false)
        {
            return Convert.ToInt64(value);
        }

        var parts = value.Split(" ");

        switch (parts[1])
        {
            case "+":
                return GetMonkeyValue(parts[0]) + GetMonkeyValue(parts[2]);
            case "-":
                return GetMonkeyValue(parts[0]) - GetMonkeyValue(parts[2]);
            case "/":
                return GetMonkeyValue(parts[0]) / GetMonkeyValue(parts[2]);
            case "*":
                return GetMonkeyValue(parts[0]) * GetMonkeyValue(parts[2]);
            default:
                throw new Exception("Unknown operator");
        }
    }
}