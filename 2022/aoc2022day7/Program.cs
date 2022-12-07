// See https://aka.ms/new-console-template for more information

using Directory = aoc2022day7.Directory;

class Program
{
    static void Main(string[] args)
    {
        string filename = args[0];
        string[] lines = File.ReadAllLines(filename);

        Directory rootDir = null;
        Directory currentDir = null;

        foreach (string line in lines)
        {
            if (line == "$ cd /")
            {
                Directory dir = new Directory("/", null);
                rootDir = dir;
                currentDir = dir;
                continue;
            }
            
            if (currentDir == null)
            {
                throw new Exception("No current dir set, cannot add file!");
            }

            if (line == "$ cd ..")
            {
                Directory parentDir = currentDir.GetParent();
                
                if (parentDir == null)
                {
                    throw new Exception($"Parent dir of {currentDir.Name()} is null");
                }

                currentDir = parentDir;
                continue;
            }

            if (line.StartsWith("$ cd "))
            {
                foreach (Directory dir in currentDir.GetDirectories())
                {
                    if (dir.Name() == line.Substring(5))
                    {
                        currentDir = dir;
                    }
                }
                continue;
            }

            if (line == "$ ls")
            {
                continue;
            }

            if (line.StartsWith("dir "))
            {
                Directory dir = new Directory(line.Substring(4), currentDir); // skip "dir "
                currentDir.AddDirectory(dir);
                continue;
            }

            string[] fileParts = line.Split(' ');
            aoc2022day7.File file = new aoc2022day7.File(fileParts[1], fileParts[0]);
            currentDir.AddFile(file);
        }
        
        if (rootDir == null)
        {
            throw new Exception("No root dir set!");
        }

        // rootDir.Debug();
        int size = rootDir.SumSizesSmallerThan(100000);
        Console.WriteLine($"Answer #1 is {size}");

        int availableSpace = 70000000;
        int neededSpace = 30000000;
        int usedSpace = rootDir.Size();

        int freeSpace = availableSpace - usedSpace;
        int freeUp = neededSpace - freeSpace;
        
        Console.WriteLine($"Free space is {freeSpace}");
        Console.WriteLine($"Free up {freeUp}");

        List<Directory> largeEnoughDirs = new List<Directory>(); 
        rootDir.ListSizesLargerThan(freeUp, largeEnoughDirs);
        List<Directory> sortedList = largeEnoughDirs.OrderBy(o=>o.Size()).ToList();

        foreach (Directory dir in sortedList)
        {
            Console.WriteLine($"Directory {dir.Name()} = {dir.Size()}");
        }
        
        Console.WriteLine($"Answer #2 is {sortedList.First().Size()}");
    }
}