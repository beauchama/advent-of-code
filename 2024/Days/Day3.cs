using System.Text.RegularExpressions;

namespace Advent.Days;

internal sealed partial class Day3
{
    private readonly string _input = File.ReadAllText("inputs/day3.txt");

    [GeneratedRegex(@"mul\((?<left>\d{1,3}),(?<right>\d{1,3})\)")]
    private static partial Regex MultiplyRegex { get; }

    public int SolvePartOne()
    {
        int result = 0;
        foreach(Match match in MultiplyRegex.Matches(_input))
        {
            int left = int.Parse(match.Groups["left"].Value);
            int right = int.Parse(match.Groups["right"].Value);

            result += left * right;
        }

        return result;
    }
}
