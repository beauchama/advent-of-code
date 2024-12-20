﻿using System.Drawing;

namespace Advent.Days;

internal sealed class Day6
{
    private const char Obstacle = '#';

    private readonly char[][] _map;
    private readonly Guard _guard;

    public Day6() => (_map, _guard) = ReadInputs();

    public int SolvePartOne()
    {
        while(IsInBounds(_map, _guard.Position))
        {
            (int x, int y) = _guard.Position;

            char current = _guard.CurrentDirection switch
            {
                Guard.Direction.Up => _map[x - 1][y],
                Guard.Direction.Down => _map[x + 1][y],
                Guard.Direction.Left => _map[x][y - 1],
                Guard.Direction.Right => _map[x][y + 1],
                _ => throw new InvalidOperationException()
            };

            if (_guard.TryMove(current == Obstacle))
                _map[x][y] = 'X';
        }

        return _map.Sum(row => row.Count(cell => cell == 'X')) + 1;

        static bool IsInBounds(char[][] map, Guard.Point position)
        {
            return position.X > 0
                && position.X < map.Length - 1
                && position.Y > 0
                && position.Y < map[position.X].Length - 1;
        }
    }

    public int SolvePartTwo()
    {
        int sum = 0;
        return sum;
    }

    private static (char[][] map, Guard guard) ReadInputs()
    {
        ReadOnlySpan<string> lines = File.ReadAllLines("inputs/day6.txt");

        char[][] map = new char[lines.Length][];
        (int x, int y) startingPosition = (0, 0);

        for (int x = 0; x < lines.Length; x++)
        {
            map[x] = new char[lines[x].Length];
            for (int y = 0; y < lines[x].Length; y++)
            {
                map[x][y] = lines[x][y];

                if (map[x][y] == '^')
                    startingPosition = (x, y);
            }
        }

        return (map, new Guard(startingPosition.x, startingPosition.y));
    }

    private class Guard(int x, int y)
    {
        private Point _position = new(x, y);
        private Direction _direction;

        public Point Position => _position;

        public Direction CurrentDirection => _direction;

        public bool TryMove(bool hasObstacle)
        {
            if (hasObstacle)
            {
                Turn();
                return false;
            }

            _position = _direction switch
            {
                Direction.Up => _position with { X = _position.X - 1 },
                Direction.Down => _position with { X = _position.X + 1 },
                Direction.Left => _position with { Y = _position.Y - 1 },
                Direction.Right => _position with { Y = _position.Y + 1 },
                _ => throw new InvalidOperationException()
            };

            return true;
        }

        private void Turn()
        {
            _direction = _direction switch
            {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                _ => throw new InvalidOperationException()
            };
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public sealed record Point(int X, int Y);
    }
}
