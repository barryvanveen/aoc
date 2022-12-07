namespace aoc2022day7;

public class Directory
{
    private string _name;
    private List<Directory> _dirs;
    private List<File> _files;
    private Directory? _parent;

    public Directory(string name, Directory? parent)
    {
        _name = name;
        _dirs = new List<Directory>();
        _files = new List<File>();
        _parent = parent;
    }

    public void AddDirectory(Directory dir)
    {
        _dirs.Add(dir);
    }

    public void AddFile(File file)
    {
        _files.Add(file);
    }

    public List<Directory> GetDirectories()
    {
        return _dirs;
    }

    public List<File> GetFiles()
    {
        return _files;
    }

    public Directory? GetParent()
    {
        return _parent;
    }

    public int Size()
    {
        int sum = 0;

        foreach (File file in _files)
        {
            sum += file.Size();
        }

        foreach (Directory dir in _dirs)
        {
            sum += dir.Size();
        }

        return sum;
    }

    public int SumSizesSmallerThan(int threshold)
    {
        int sum = 0;
        
        if (Size() <= threshold)
        {
            sum += Size();
        }

        foreach (Directory dir in _dirs)
        {
            sum += dir.SumSizesSmallerThan(100000);
        }

        return sum;
    }

    public List<Directory> ListSizesLargerThan(int threshold, List<Directory> list)
    {
        if (Size() >= threshold)
        {
            list.Add(this);
        }

        foreach (Directory subdir in _dirs)
        {
            subdir.ListSizesLargerThan(threshold, list);
        }

        return list;
    }

    public string Name()
    {
        return _name;
    }

    public void Debug()
    {
        Console.WriteLine($"- {_name} (dir)");

        Console.WriteLine("' --> indent '");
        foreach (Directory dir in _dirs)
        {
            dir.Debug();
        }

        foreach (File file in _files)
        {
            Console.WriteLine($"\t - {file.Name()} (file, size={file.Size()})");
        }
        Console.WriteLine("' <-- unindent '");
    }
}