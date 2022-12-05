namespace aoc2022day5;

public struct Step
{
    private readonly int _amount;
    private readonly int _from;
    private readonly int _to;

    public Step(string amount, string from, string to)
    {
        _amount = Convert.ToInt32(amount);
        _from = Convert.ToInt32(from) - 1;
        _to = Convert.ToInt32(to) - 1;
    }

    public int Amount()
    {
        return _amount;
    }
    
    public int From()
    {
        return _from;
    }
    
    public int To()
    {
        return _to;
    }
}