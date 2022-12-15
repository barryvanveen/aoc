namespace aoc2022day11;

public class Monkey
{
    private Queue<Int64> _items;
    private Func<Int64, Int64> _operation;
    private Func<Int64, Int64> _relief;
    private int _test;
    private int _testTrueMonkey;
    private int _testFalseMonkey;
    private Int64 _inspections = 0;
    
    public Monkey(
        List<Int64> items,
        Func<Int64, Int64> operation,
        Func<Int64, Int64> relief,
        int test,
        int testTrueMonkey,
        int testFalseMonkey
    )
    {
        _items = new Queue<Int64>(items);
        _operation = operation;
        _relief = relief;
        _test = test;
        _testTrueMonkey = testTrueMonkey;
        _testFalseMonkey = testFalseMonkey;
    }

    public MonkeyThrow InspectAndThrow()
    {
        Int64 item = _items.Dequeue();
        _inspections++;

        // operation
        item = _operation(item);

        // relief
        item = _relief(item);

        // test
        if (item % _test == 0)
        {
            // throw
            return new MonkeyThrow(item, _testTrueMonkey);
        } 

        // throw
        return new MonkeyThrow(item, _testFalseMonkey);
    }

    public void CatchItem(Int64 item)
    {
        _items.Enqueue(item);
    }

    public Int64 Inspections()
    {
        return _inspections;
    }
}