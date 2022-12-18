namespace Aoc22_17;

using System.Text;

public class Pit
{
	private readonly List<bool[]> pitRows = new();

	public Pit(Rock[] rocks, int[] jets, int width = 7, int startLeft = 2, int startBottom = 3)
	{
		this.Rocks = rocks;
		this.Jets = jets;
		this.Width = width;
		this.StartLeft = startLeft;
		this.StartBottom = startBottom;
	}

	public int Width { get; }

	public int StartLeft { get; }

	public int StartBottom { get; }

	public Rock[] Rocks { get; }

	public int[] Jets { get; }

	public int TowerHeight { get; private set; }

	public bool this[int x, int y]
	{
		get
		{
			if (y + 1 > this.pitRows.Count)
			{
				return false;
			}

			return this.pitRows[y][x];
		}
		set
		{
			this.EnsurePitHeight(y + 1);
			this.pitRows[y][x] = value;
		}
	}

	public void Simulate(int numberOfRocks)
	{
		int rockIndex = 0;
		int jetIndex = 0;
		int rockBottom = 0;
		int rockLeft = 0;
		bool isFalling = false;

		int rockCount = 0;

		while (rockCount < numberOfRocks)
		{
			if (!isFalling)
			{
				// drop new rock into pit
				rockBottom = this.TowerHeight + this.StartBottom;
				rockLeft = this.StartLeft;
				isFalling = true;
			}

			Rock rock = this.Rocks[rockIndex];

			// apply sideways jet movement
			int jet = this.Jets[jetIndex];

			// check for collision with side walls
			if (!this.IsCollidingSideways(rock, rockLeft, rockBottom, jet))
			{
				rockLeft += jet;
			}

			jetIndex = (jetIndex + 1) % this.Jets.Length;

			// apply downward movement

			// check for collisions
			if (this.IsCollidingDownward(rock, rockLeft, rockBottom))
			{
				this.PileUp(rock, rockLeft, rockBottom);
				isFalling = false;
				rockIndex = (rockIndex + 1) % this.Rocks.Length;
				rockCount++;
			}
			else
			{
				rockBottom--;
			}
		}
	}

	private bool IsCollidingSideways(Rock rock, int rockLeft, int rockBottom, int jet)
	{
		if (rockLeft + jet < 0 || rockLeft + rock.Width + jet > this.Width)
		{
			return true;
		}

		for (int y = 0; y < rock.Height; y++)
		{
			for (int x = 0; x < rock.Width; x++)
			{
				if (rock.Shapes[x, y] == RockShape.Rock && this[x + rockLeft + jet, y + rockBottom])
				{
					// collision with another rock
					return true;
				}
			}
		}

		return false;
	}

	private bool IsCollidingDownward(Rock rock, int rockLeft, int rockBottom)
	{
		if (rockBottom == 0)
		{
			// collision with the ground
			return true;
		}

		for (int y = 0; y < rock.Height; y++)
		{
			for (int x = 0; x < rock.Width; x++)
			{
				if (rock.Shapes[x, y] == RockShape.Rock && this[x + rockLeft, y + rockBottom - 1])
				{
					// collision with another rock
					return true;
				}
			}
		}

		return false;
	}

	private void PileUp(Rock rock, int rockLeft, int rockBottom)
	{
		for (int y = 0; y < rock.Height; y++)
		{
			for (int x = 0; x < rock.Width; x++)
			{
				if (rock.Shapes[x, y] != RockShape.Air)
				{
					int h = y + rockBottom;
					this[x + rockLeft, h] = true;
					this.TowerHeight = Math.Max(h + 1, this.TowerHeight);
				}
			}
		}

		//this.DebugDisplay();
	}

	private void EnsurePitHeight(int height)
	{
		if (this.pitRows.Count >= height)
		{
			return;
		}

		this.pitRows.EnsureCapacity(height);
		this.pitRows.AddRange(Enumerable.Range(0, height - this.pitRows.Count).Select(_ => new bool[this.Width]));
	}

	public void DebugDisplay()
	{
		StringBuilder builder = new();

		for (int i = this.pitRows.Count - 1; i >= 0; i--)
		{
			char[] line = this.pitRows[i].Select(b => b ? '#' : '.').ToArray();
			builder.AppendLine($"|{new string(line)}| {i}");
		}

		builder.AppendLine($"|{new string(Enumerable.Repeat('-', this.Width).ToArray())}|");
		Console.Write(builder.ToString());
		Console.WriteLine();
		Console.WriteLine();
	}
}














