namespace Advent.Days;

internal sealed class Day2
{
    private readonly IEnumerable<int[]> _inputs = ReadInputs();

    public int SolvePartOne() => _inputs.Count(l => IsSafe(l));

    public int SolvePartTwo()
    {
        return _inputs.Count(l => IsSafeWithDampener(l));

        static bool IsSafeWithDampener(ReadOnlySpan<int> report, int index = 0)
        {
            if (index >= report.Length)
                return false;

            List<int> next = [.. report];
            next.RemoveAt(index);

            return IsSafe(next.ToArray()) || IsSafeWithDampener(report, index + 1);
        }
    }

    private static bool IsSafe(ReadOnlySpan<int> report)
    {
        bool? increasing = null;

        for (int i = 1; i < report.Length; i++)
        {
            int difference = report[i] - report[i - 1];

            if (Math.Abs(difference) is < 1 or > 3)
                return false;

            if (difference > 0)
            {
                increasing ??= true;

                if (!increasing.Value)
                    return false;
            }
            else
            {
                increasing ??= false;

                if (increasing.Value)
                    return false;
            }
        }

        return true;
    }

    private static List<int[]> ReadInputs()
    {
        ReadOnlySpan<string> inputs = File.ReadAllLines("inputs/day2.txt");
        List<int[]> reports = new(inputs.Length);
        List<int> levels = [];

        foreach (ReadOnlySpan<char> input in inputs)
        {
            foreach (Range range in input.Split(' '))
            {
                levels.Add(int.Parse(input[range]));
            }

            reports.Add([.. levels]);
            levels.Clear();
        }

        return reports;
    }
}
