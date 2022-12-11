namespace aoc2022day9;

public class Coordinate
{
    private int _x;
    private int _y;
    
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

	public Coordinate Move(Direction direction)
	{
		switch (direction)
		{
			case Direction.Up:
				return new Coordinate(_x, _y + 1);
			case Direction.Right:
				return new Coordinate(_x + 1, _y);
			case Direction.Down:
				return new Coordinate(_x, _y - 1);
			case Direction.Left:
				return new Coordinate(_x - 1, _y);
			default:
				throw new Exception($"Unknown direction {direction}");
		}
	}

	public Coordinate Follow(Coordinate head)
	{
		int xDiff = head.X() - _x;
		int yDiff = head.Y() - _y;

		// Console.WriteLine($"Diff is {xDiff},{yDiff}");

		// x and y too far
		if (Math.Abs(xDiff) > 1 && Math.Abs(yDiff) > 1) 
		{
			int newX, newY;

			switch (Math.Sign(xDiff))
			{
				case -1:
					newX = _x - 1;
					break;
				case 1:
					newX = _x + 1;
					break;
				default:
					throw new Exception("xDiff cannot be equal");
			}
			
			switch (Math.Sign(yDiff))
			{
				case -1:
					newY = _y - 1;
					break;
				case 1:
					newY = _y + 1;
					break;
				default:
					throw new Exception("yDiff cannot be equal");
			}

			return new Coordinate(newX, newY);
		}

		if (Math.Abs(xDiff) > 1 && Math.Abs(yDiff) == 1 || Math.Abs(xDiff) == 1 && Math.Abs(yDiff) > 1)
		{
			int newX, newY;

			switch (Math.Sign(xDiff))
			{
				case -1:
					newX = _x - 1;
					break;
				case 1:
					newX = _x + 1;
					break;
				default:
					throw new Exception("xDiff cannot be equal");
			}
			
			switch (Math.Sign(yDiff))
			{
				case -1:
					newY = _y - 1;
					break;
				case 1:
					newY = _y + 1;
					break;
				default:
					throw new Exception("yDiff cannot be equal");
			}

			return new Coordinate(newX, newY);
		}

		// x too far
		if (Math.Abs(xDiff) > 1)
		{
			switch (Math.Sign(xDiff))
			{
				case -1:
					return new Coordinate(_x - 1, _y);
				case 1:
					return new Coordinate(_x + 1, _y);
				default:
					throw new Exception("xDiff cannot be equal");
			}
		}
		
		// y too far
		if (Math.Abs(yDiff) > 1)
		{
			switch (Math.Sign(yDiff))
			{
				case -1:
					return new Coordinate(_x, _y - 1);
				case 1:
					return new Coordinate(_x, _y + 1);
				default:
					throw new Exception("yDiff cannot be equal");
			}
		}

		return new Coordinate(_x, _y);
	}

	public override string ToString()
	{
		return $"{_x},{_y}";
	}	
}