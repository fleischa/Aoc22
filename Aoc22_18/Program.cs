namespace Aoc22_18;

using AocCommon;

internal class Program
{
	private static async Task Main(string[] args)
	{
		List<(int x, int y, int z)> blockList = new();

		await new SingleLineProcessor<object>((line, _) =>
		{
			string[] t = line.Split(',');
			blockList.Add((int.Parse(t[0]), int.Parse(t[1]), int.Parse(t[2])));
		}).Process();

		int minX = blockList.Min(b => b.x);
		int maxX = blockList.Max(b => b.x);
		int offsetX = 2 - minX;

		int minY = blockList.Min(b => b.y);
		int maxY = blockList.Max(b => b.y);
		int offsetY = 2 - minY;

		int minZ = blockList.Min(b => b.z);
		int maxZ = blockList.Max(b => b.z);
		int offsetZ = 2 - minZ;

		Block[,,] blocks = new Block[maxX - minX + 5, maxY - minY + 5, maxZ - minZ + 5];

		foreach ((int x, int y, int z) b in blockList)
		{
			blocks[b.x + offsetX, b.y + offsetY, b.z + offsetZ] = Block.Obsidian;
		}

		Program.Flood(blocks, 0, 0, 0);

		int faces = 0;

		for (int z = 1; z < blocks.GetLength(2) - 1; z++)
		{
			for (int y = 1; y < blocks.GetLength(1) - 1; y++)
			{
				for (int x = 1; x < blocks.GetLength(0) - 1; x++)
				{
					if (blocks[x, y, z] != Block.Water)
					{
						continue;
					}

					if (blocks[x - 1, y, z] == Block.Obsidian)
					{
						faces++;
					}

					if (blocks[x + 1, y, z] == Block.Obsidian)
					{
						faces++;
					}

					if (blocks[x, y - 1, z] == Block.Obsidian)
					{
						faces++;
					}

					if (blocks[x, y + 1, z] == Block.Obsidian)
					{
						faces++;
					}

					if (blocks[x, y, z - 1] == Block.Obsidian)
					{
						faces++;
					}

					if (blocks[x, y, z + 1] == Block.Obsidian)
					{
						faces++;
					}
				}
			}
		}

		Console.WriteLine(faces);
	}

	private static void Flood(Block[,,] blocks, int x, int y, int z)
	{
		if (x < 0 || y < 0 || z < 0 || x >= blocks.GetLength(0) || y >= blocks.GetLength(1) || z >= blocks.GetLength(2))
		{
			return;
		}

		Block block = blocks[x, y, z];

		if (block is Block.Obsidian or Block.Water)
		{
			return;
		}

		blocks[x, y, z] = Block.Water;

		Program.Flood(blocks, x - 1, y, z);
		Program.Flood(blocks, x + 1, y, z);
		Program.Flood(blocks, x, y - 1, z);
		Program.Flood(blocks, x, y + 1, z);
		Program.Flood(blocks, x, y, z - 1);
		Program.Flood(blocks, x, y, z + 1);
	}
}
