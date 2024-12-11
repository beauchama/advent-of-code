namespace Advent.Days;

internal class Day1
{
    private readonly (int left, int right)[] _inputs = ReadInputs();

    public int SolvePartOne()
    {
        return _inputs.Sum(i => Math.Abs(i.left - i.right));
    }

    public int SolvePartTwo()
    {
        IEnumerable<int> lefts = _inputs.Select(i => i.left);

        int score = 0;
        foreach(int left in lefts)
        {
            score += left * _inputs.Count(i => i.right == left);
        }

        return score;
    }

    private static (int left, int right)[] ReadInputs()
    {
        ReadOnlySpan<string> inputs = File.ReadAllLines("inputs/day1.txt");
        Span<Range> ranges = stackalloc Range[2];

        List<int> left = new(inputs.Length);
        List<int> right = new(inputs.Length);

        foreach (ReadOnlySpan<char> input in inputs)
        {
            _ = input.Split(ranges, ' ', StringSplitOptions.RemoveEmptyEntries);

            left.Add(int.Parse(input[ranges[0]]));
            right.Add(int.Parse(input[ranges[1]]));
        }

        return [.. left.Order().Zip(right.Order())];
    }
}
