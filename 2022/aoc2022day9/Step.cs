namespace aoc2022day9;

public class Step
{
    private Direction _direction;
    private int _size;
    
    public Step(string direction, string size)
    {
        switch (direction)
        {
            case "U":
                _direction = Direction.Up;
                break;
			case "R":
                _direction = Direction.Right;
                break;
			case "D":
                _direction = Direction.Down;
                break;
			case "L":
                _direction = Direction.Left;
                break;
			default:
                throw new Exception($"Unknown direction {direction}");
        }

        _size = Convert.ToInt32(size);
    }

    public Direction GetDirection()
    {
	    return _direction;
    }

    public int GetSize()
    {
	    return _size;
    }

    public void Debug()
	{
		Console.WriteLine($"==> {_direction} {_size}");
	}
}