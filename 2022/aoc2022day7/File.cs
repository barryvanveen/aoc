namespace aoc2022day7;

public class File
{
    private string _name;
    private int _size;
    
    public File(string name, string size)
    {
        _name = name;
        _size = Convert.ToInt32(size);
    }

    public int Size()
    {
        return _size;
    }

    public string Name()
    {
        return _name;
    }
}