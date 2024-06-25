using System.Collections.Generic;
using UnityEngine;

public static class PatternRecognition
{
    // Define patterns relative to the top-left cell of the pattern
    public static readonly Dictionary<string, Vector2Int[]> patterns = new Dictionary<string, Vector2Int[]>
    {
        // Still lifes
        { "Block", new[] { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(1, 1) } },
        { "Tub", new[] { new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(2, 1), new Vector2Int(1, 2) } },
        // Oscillators
        { "Blinker", new[] { new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(2, 1) } },
        { "Toad", new[] { new Vector2Int(1, 0), new Vector2Int(2, 0), new Vector2Int(3, 0), new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(2, 1) } }
        // Add more patterns as needed
    };

    public static string DetectPattern(Cell[,] grid, int x, int y)
    {
        foreach (var pattern in patterns)
        {
            if (IsPatternMatch(grid, x, y, pattern.Value))
            {
                return pattern.Key;
            }
        }
        return null;
    }

    private static bool IsPatternMatch(Cell[,] grid, int x, int y, Vector2Int[] pattern)
    {
        foreach (var offset in pattern)
        {
            int newX = x + offset.x;
            int newY = y + offset.y;
            if (newX < 0 || newY < 0 || newX >= grid.GetLength(0) || newY >= grid.GetLength(1))
            {
                return false;
            }
            if (!grid[newX, newY].isAlive)
            {
                return false;
            }
        }
        return true;
    }
}
