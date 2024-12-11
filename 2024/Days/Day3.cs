using System.Text.RegularExpressions;

namespace Advent.Days;

internal sealed partial class Day3
{
    private readonly string _input = File.ReadAllText("inputs/day3.txt");

    [GeneratedRegex(@"mul\((?<left>\d{1,3}),(?<right>\d{1,3})\)")]
    private static partial Regex MultiplyRegex { get; }

    [GeneratedRegex(@"mul\((?<left>\d{1,3}),(?<right>\d{1,3})\)|do\(\)|don't\(\)")]
    private static partial Regex EnabledMultiplyRegex { get; }

    public int SolvePartOne()
    {
        int result = 0;
        foreach (Match match in MultiplyRegex.Matches(_input))
        {
            int left = int.Parse(match.Groups["left"].Value);
            int right = int.Parse(match.Groups["right"].Value);

            result += left * right;
        }

        return result;
    }

    public int SolvePartTwo()
    {
        bool enabled = true;
        int result = 0;

        foreach (Match match in EnabledMultiplyRegex.Matches(_input))
        {
            if (match.Value == "don't()")
            {
                enabled = false;
            }
            else if (match.Value == "do()")
            {
                enabled = true;
                continue;
            }

            if (enabled)
            {
                int left = int.Parse(match.Groups["left"].Value);
                int right = int.Parse(match.Groups["right"].Value);
                result += left * right;
            }
        }

        return result;
    }
}
