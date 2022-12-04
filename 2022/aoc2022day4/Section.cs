namespace aoc2022day2;

public struct Section
{
    private readonly int _start;
    private readonly int _end;

    public Section(string start, string end)
    {
        _start = Convert.ToInt32(start);
        _end = Convert.ToInt32(end);
    }

    public int Start()
    {
        return _start;
    }
    
    public int End()
    {
        return _end;
    }
    
    public bool Contains(Section other)
    {
        return (_start <= other.Start() && _end >= other.End());
    }

    public bool Overlaps(Section other)
    {
        if (_start <= other.Start() && _end >= other.Start())
        {
            return true;
        }

        if (_start <= other.End() && _end >= other.End())
        {
            return true;
        }

        return _start >= other.Start() && _end <= other.End();
    }
}