namespace Aoc22_08;

internal class Program
{
    private static async Task Main(string[] args)
    {
        List<int[]> forestList = new();

        using StreamReader reader = File.OpenText("input.txt");
        while (await reader.ReadLineAsync() is { } line)
        {
            forestList.Add(line.Select(c => int.Parse(c.ToString())).ToArray());
        }

        int width = forestList.Max(r => r.Length);
        int height = forestList.Count;

        int[,] forest = new int[width, height];

        for (int y = 0; y < height; y++)
        {
            int[] row = forestList[y];
            for (int x = 0; x < width; x++)
            {
                forest[x, y] = row[x];
            }
        }

        int visible = 0;
        int maxScenicScore = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (Program.IsVisible(x, y, forest))
                {
                    visible++;
                }

                maxScenicScore = Math.Max(Program.GetScenicScore(x, y, forest), maxScenicScore);
            }
        }

        Console.WriteLine($"visible trees: {visible}");
        Console.WriteLine($"max scenic score: {maxScenicScore}");
    }

    private static bool IsVisible(int treeX, int treeY, int[,] forest)
    {
        if (treeX == 0 || treeY == 0 || treeX == forest.GetLength(0) - 1 || treeY == forest.GetLength(1) - 1)
        {
            return true;
        }

        int height = forest[treeX, treeY];

        bool isVisible = true;
        for (int x = 0; x < treeX; x++)
        {
            if (forest[x, treeY] >= height)
            {
                isVisible = false;
                break;
            }
        }

        if (isVisible)
        {
            return true;
        }

        isVisible = true;
        for (int x = treeX + 1; x < forest.GetLength(0); x++)
        {
            if (forest[x, treeY] >= height)
            {
                isVisible = false;
                break;
            }
        }

        if (isVisible)
        {
            return true;
        }

        isVisible = true;
        for (int y = 0; y < treeY; y++)
        {
            if (forest[treeX, y] >= height)
            {
                isVisible = false;
                break;
            }
        }

        if (isVisible)
        {
            return true;
        }

        isVisible = true;
        for (int y = treeY + 1; y < forest.GetLength(1); y++)
        {
            if (forest[treeX, y] >= height)
            {
                isVisible = false;
                break;
            }
        }

        return isVisible;
    }

    private static int GetScenicScore(int treeX, int treeY, int[,] forest)
    {
        int score = 1;
        int height = forest[treeX, treeY];

        bool isVisible = true;
        for (int x = treeX - 1; x >= 0; x--)
        {
            if (forest[x, treeY] >= height)
            {
                score *= treeX - x;
                isVisible = false;
                break;
            }
        }

        if (isVisible)
        {
            score *= treeX;
        }

        isVisible = true;
        for (int x = treeX + 1; x < forest.GetLength(0); x++)
        {
            if (forest[x, treeY] >= height)
            {
                score *= x - treeX;
                isVisible = false;
                break;
            }
        }

        if (isVisible)
        {
            score *= forest.GetLength(0) - treeX - 1;
        }

        isVisible = true;
        for (int y = treeY - 1; y >= 0; y--)
        {
            if (forest[treeX, y] >= height)
            {
                score *= treeY - y;
                isVisible = false;
                break;
            }
        }

        if (isVisible)
        {
            score *= treeY;
        }

        isVisible = true;
        for (int y = treeY + 1; y < forest.GetLength(1); y++)
        {
            if (forest[treeX, y] >= height)
            {
                score *= y - treeY;
                isVisible = false;
                break;
            }
        }

        if (isVisible)
        {
            score *= forest.GetLength(1) - treeY - 1;
        }

        return score;
    }
}











