// See https://aka.ms/new-console-template for more information

class Program
{
    static void Main(string[] args)
    {
        string filename = args[0];
        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            // var marker = FindMarkerStart(line);
            // Console.WriteLine($"Answer #1 is {marker}");

            var marker = FindMessageStart(line);
            Console.WriteLine($"Answer #2 is {marker}");
        }
    }

    private static int FindMarkerStart(string line)
    {
        int markerLength = 4;
        int markerStart = 0;
        
        for (int x=markerLength-1; x<line.Length; x++)
        {
            char[] test = new char[markerLength];
            for (int y = markerLength-1; y >= 0; y--)
            {
                test[y] = line[x-y];
            }
            
            if (test.Distinct().Count() == 4)
            {
                markerStart = x + 1;
                break;
            }
        }

        if (markerStart == 0)
        {
            throw new Exception("No marker found!");
        }

        return markerStart;
    }
    
    private static int FindMessageStart(string line)
    {
        int markerLength = 14;
        int markerStart = 0;
        
        for (int x=markerLength-1; x<line.Length; x++)
        {
            char[] test = new char[markerLength];
            for (int y = markerLength-1; y >= 0; y--)
            {
                test[y] = line[x-y];
            }

            if (test.Distinct().Count() == markerLength)
            {
                markerStart = x + 1;
                break;
            }
        }

        if (markerStart == 0)
        {
            throw new Exception("No marker found!");
        }

        return markerStart;
    }
}