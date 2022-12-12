namespace Aoc22_12;

using AocCommon;

internal class Program
{
	private static async Task Main(string[] args)
	{
		List<string> lines = new();
		await new SingleLineProcessor<object>((line, _) => lines.Add(line)).Process();
		char[][] input = lines.Select(l => l.Select(c => c).ToArray()).ToArray();

		TerrainNode[,] terrainNodes = new TerrainNode[input.Length, input[0].Length];
		List<TerrainNode> startNodes = new();
		TerrainNode endNode = null!;

		for (int x = 0; x < input.Length; x++)
		{
			for (int y = 0; y < input[0].Length; y++)
			{
				char c = input[x][y];
				TerrainNode node = new(c);
				switch (c)
				{
					case 'a':
						startNodes.Add(node);
						break;
					case 'S':
						node.Height = 'a';
						node.IsMainStart = true;
						startNodes.Add(node);
						break;
					case 'E':
						node.Height = 'z';
						node.IsEnd = true;
						endNode = node;
						break;
				}

				terrainNodes[x, y] = node;
			}
		}

		for (int x = 0; x < terrainNodes.GetLength(0); x++)
		{
			for (int y = 0; y < terrainNodes.GetLength(1); y++)
			{
				TerrainNode node = terrainNodes[x, y];

				if (x > 0)
				{
					node.Neighbors.Add(terrainNodes[x - 1, y]);
				}

				if (y > 0)
				{
					node.Neighbors.Add(terrainNodes[x, y - 1]);
				}

				if (x < terrainNodes.GetLength(0) - 1)
				{
					node.Neighbors.Add(terrainNodes[x + 1, y]);
				}

				if (y < terrainNodes.GetLength(1) - 1)
				{
					node.Neighbors.Add(terrainNodes[x, y + 1]);
				}
			}
		}

		int minPathLength = int.MaxValue;

		foreach (TerrainNode startNode in startNodes)
		{
			startNode.Start();

			if (startNode.IsMainStart)
			{
				Console.WriteLine($"main start: {endNode.PathLength}");
			}

			minPathLength = Math.Min(endNode.PathLength, minPathLength);
			Program.ClearPathLenghts(terrainNodes);
		}

		Console.WriteLine($"scenic start: {minPathLength}");
	}

	private static void ClearPathLenghts(TerrainNode[,] terrainNodes)
	{
		for (int x = 0; x < terrainNodes.GetLength(0); x++)
		{
			for (int y = 0; y < terrainNodes.GetLength(1); y++)
			{
				terrainNodes[x, y].PathLength = int.MaxValue;
			}
		}
	}
}















