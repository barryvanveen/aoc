namespace aoc2022day11;

public struct MonkeyThrow
{
    public Int64 Item { get; }
    public int ToMonkey { get; }

    public MonkeyThrow(
        Int64 item,
        int toMonkey
    )
    {
        Item = item;
        ToMonkey = toMonkey;
    }
}