namespace Advent.Days;

internal sealed class Day5
{
    private readonly (int left, int right)[] _rules;
    private readonly int[][] _updates;

    public Day5() => (_rules, _updates) = ReadInputs();

    public int SolvePartOne()
    {
        int sum = 0;

        foreach (int[] update in _updates.Where(IsCorrectlyOrdered))
            sum += update[update.Length / 2];

        return sum;
    }

    public int SolvePartTwo()
    {
        int sum = 0;

        foreach (int[] update in _updates.Where(x => !IsCorrectlyOrdered(x)))
        {
            sum += SortCorrectly(update);
        }

        return sum;

        int SortCorrectly(int[] update)
        {
            Dictionary<int, int> pages = update.Select((page, index) => (page, index)).ToDictionary();

            Array.Sort(update, Comparer<int>.Create((before, after) =>
            {
                if (!pages.ContainsKey(before) || !pages.ContainsKey(after))
                    return 0;

                if (_rules.Any(rule => rule.left == before && rule.right == after))
                    return -1;

                if (_rules.Any(rule => rule.left == after && rule.right == before))
                    return 1;

                return 0;
            }));

            return update[update.Length / 2];
        }
    }

    private bool IsCorrectlyOrdered(int[] update)
    {
        Dictionary<int, int> pages = update.Select((page, index) => (page, index)).ToDictionary();

        foreach ((int before, int after) in _rules)
        {
            if (!pages.ContainsKey(before) || !pages.ContainsKey(after))
                continue;

            if (pages[before] > pages[after])
                return false;
        }

        return true;
    }

    private ((int left, int right)[], int[][]) ReadInputs()
    {
        ReadOnlySpan<string> inputs = File.ReadAllLines("inputs/day5.txt");
        ReadOnlySpan<string> ruleInputs = inputs[..inputs.LastIndexOf(string.Empty)];
        ReadOnlySpan<string> updateInputs = inputs[(inputs.LastIndexOf(string.Empty) + 1)..];

        List<(int left, int right)> rules = new(ruleInputs.Length);

        Span<Range> ruleRanges = stackalloc Range[2];
        foreach (ReadOnlySpan<char> input in ruleInputs)
        {
            _ = input.Split(ruleRanges, '|', StringSplitOptions.RemoveEmptyEntries);

            int left = int.Parse(input[ruleRanges[0]]);
            int right = int.Parse(input[ruleRanges[1]]);

            rules.Add((left, right));
        }

        List<int[]> updates = new(updateInputs.Length);
        List<int> row = [];

        foreach (ReadOnlySpan<char> input in updateInputs)
        {
            foreach (Range range in input.Split(','))
            {
                row.Add(int.Parse(input[range]));
            }

            updates.Add([.. row]);
            row.Clear();
        }

        return ([.. rules], [.. updates]);
    }
}
