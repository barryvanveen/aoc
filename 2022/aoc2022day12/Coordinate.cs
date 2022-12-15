namespace aoc2022day12;

public class Coordinate: IEquatable<Coordinate>
{
    private readonly int _x;
    private readonly int _y;
    
    public Coordinate(int x, int y)
    {
	    _x = x;
	    _y = y;
    }

    public int X()
    {
	    return _x;
    }

    public int Y()
    {
	    return _y;
    }

	public string Debug()
	{
		return $"{_x},{_y}";
	}

	public override string ToString()
	{
		return $"{_x},{_y}";
	}

	public bool Equals(Coordinate? other)
	{
		if (ReferenceEquals(null, other)) return false;
		if (ReferenceEquals(this, other)) return true;
		return _x == other._x && _y == other._y;
	}

	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != this.GetType()) return false;
		return Equals((Coordinate)obj);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(_x, _y);
	}
}