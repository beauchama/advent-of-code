namespace Advent.Days;

internal sealed class Day4
{
    private readonly char[][] _inputs = ReadInputs();

    public int SolvePartOne()
    {
        int count = 0;

        for (int i = 0; i < _inputs.Length; i++)
        {
            for (int j = 0; j < _inputs[i].Length; j++)
            {
                if (_inputs[i][j] is 'X')
                {
                    count += HasHorizontal(_inputs, i, j);
                    count += HasVertical(_inputs, i, j);
                    count += HasDiagonal(_inputs, i, j);
                }
            }
        }

        return count;
    }

    private static int HasHorizontal(char[][] inputs, int i, int j)
    {
        int count = 0;

        if (Right())
            count++;

        if (Left())
            count++;

        return count;

        bool Right()
        {
            if (j + 3 >= inputs[i].Length)
                return false;

            return inputs[i][j + 1] is 'M' && inputs[i][j + 2] is 'A' && inputs[i][j + 3] is 'S';
        }

        bool Left()
        {
            if (j - 3 < 0)
                return false;

            return inputs[i][j - 1] is 'M' && inputs[i][j - 2] is 'A' && inputs[i][j - 3] is 'S';
        }
    }

    private static int HasVertical(char[][] inputs, int i, int j)
    {
        int count = 0;

        if (Down())
            count++;

        if (Up())
            count++;

        return count;

        bool Down()
        {
            if (i + 3 >= inputs.Length)
                return false;

            return inputs[i + 1][j] is 'M' && inputs[i + 2][j] is 'A' && inputs[i + 3][j] is 'S';
        }

        bool Up()
        {
            if (i - 3 < 0)
                return false;

            return inputs[i - 1][j] is 'M' && inputs[i - 2][j] is 'A' && inputs[i - 3][j] is 'S';
        }
    }

    private static int HasDiagonal(char[][] inputs, int i, int j)
    {
        int count = 0;

        if (RightDownDiagonal())
            count++;

        if (RightUpDiagonal())
            count++;

        if (LeftDownDiagonal())
            count++;

        if (LeftUpDiagonal())
            count++;

        return count;

        bool RightDownDiagonal()
        {
            if (j + 3 >= inputs[i].Length || i + 3 >= inputs.Length)
                return false;

            return inputs[i + 1][j + 1] is 'M' && inputs[i + 2][j + 2] is 'A' && inputs[i + 3][j + 3] is 'S';
        }

        bool RightUpDiagonal()
        {
            if (j + 3 >= inputs[i].Length || i - 3 < 0)
                return false;

            return inputs[i - 1][j + 1] is 'M' && inputs[i - 2][j + 2] is 'A' && inputs[i - 3][j + 3] is 'S';
        }

        bool LeftDownDiagonal()
        {
            if (j - 3 < 0 || i + 3 >= inputs.Length)
                return false;

            return inputs[i + 1][j - 1] is 'M' && inputs[i + 2][j - 2] is 'A' && inputs[i + 3][j - 3] is 'S';
        }

        bool LeftUpDiagonal()
        {
            if (j - 3 < 0 || i - 3 < 0)
                return false;

            return inputs[i - 1][j - 1] is 'M' && inputs[i - 2][j - 2] is 'A' && inputs[i - 3][j - 3] is 'S';
        }
    }

    public int SolvePartTwo()
    {
        int count = 0;

        for (int i = 0; i < _inputs.Length; i++)
        {
            for (int j = 0; j < _inputs[i].Length; j++)
            {
                if (_inputs[i][j] is 'M')
                {
                    count += HasXHorizontal(_inputs, i, j);
                    count += HasXVertical(_inputs, i, j);
                }
            }
        }

        return count;
    }

    private static int HasXHorizontal(char[][] inputs, int i, int j)
    {
        int count = 0;

        if (j + 2 >= inputs[i].Length)
            return 0;

        if (Up())
            count++;

        if (Down())
            count++;

        return count++;

        bool Up()
        {
            if (i + 2 >= inputs.Length)
                return false;

            return inputs[i][j + 2] is 'M' && inputs[i + 1][j + 1] is 'A' && inputs[i + 2][j] is 'S' && inputs[i + 2][j + 2] is 'S';
        }

        bool Down()
        {
            if (i - 2 < 0)
                return false;

            return inputs[i][j + 2] is 'M' && inputs[i - 1][j + 1] is 'A' && inputs[i - 2][j] is 'S' && inputs[i - 2][j + 2] is 'S';
        }
    }

    private static int HasXVertical(char[][] inputs, int i, int j)
    {
        int count = 0;

        if (i + 2 >= inputs.Length)
            return 0;

        if (Left())
            count++;

        if (Right())
            count++;

        return count;

        bool Left()
        {
            if (j + 2 >= inputs[i].Length)
                return false;

            return inputs[i + 2][j] is 'M' && inputs[i + 1][j + 1] is 'A' && inputs[i][j + 2] is 'S' && inputs[i + 2][j + 2] is 'S';
        }

        bool Right()
        {
            if (j - 2 < 0)
                return false;

            return inputs[i + 2][j] is 'M' && inputs[i + 1][j - 1] is 'A' && inputs[i][j - 2] is 'S' && inputs[i + 2][j - 2] is 'S';
        }
    }

    private static char[][] ReadInputs()
    {
        ReadOnlySpan<string> inputs = File.ReadAllLines("inputs/day4.txt");

        char[][] words = new char[inputs.Length][];

        for (int i = 0; i < inputs.Length; i++)
            words[i] = inputs[i].ToCharArray();

        return words;
    }
}
