namespace Aoc22_14;

public class SandPit
{
	private (int x, int y)? falling;
	private int xOffset;

	public SandPit(IEnumerable<RockPath> rockPaths)
	{
		this.Initialize(rockPaths);
	}

	public SandPitState[,] SandPitStates { get; private set; } = null!;

	public int Width { get; private set; }

	public int Height { get; private set; }

	public TickResult Tick()
	{
		if (this.falling == null)
		{
			if (this.SandPitStates[500 - this.xOffset, 0] == SandPitState.Sand)
			{
				// sand has reached the top
				return new TickResult(false, null, null);
			}

			this.SandPitStates[500 - this.xOffset, 0] = SandPitState.Sand;
			this.falling = (500 - this.xOffset, 0);
			return new TickResult(true, null, this.falling);
		}

		(int x, int y) f = this.falling.Value;

		if (f.y >= this.Height - 1)
		{
			// we reached the abyss
			this.SandPitStates[f.x, f.y] = SandPitState.Air;
			return new TickResult(false, this.falling, null);
		}

		SandPitState below = this.SandPitStates[f.x, f.y + 1];

		switch (below)
		{
			case SandPitState.Air:
				// falling straight down
				this.SandPitStates[f.x, f.y] = SandPitState.Air;
				this.SandPitStates[f.x, f.y + 1] = SandPitState.Sand;
				this.falling = (f.x, f.y + 1);
				return new TickResult(true, (f.x, f.y), this.falling);
			case SandPitState.Rock:
			case SandPitState.Sand:
				if (f.x <= 0)
				{
					// falling out the left side of the pit into the abyss
					this.SandPitStates[f.x, f.y] = SandPitState.Air;
					return new TickResult(false, this.falling, null);
				}

				if (this.SandPitStates[f.x - 1, f.y + 1] == SandPitState.Air)
				{
					// sand flowing to the left
					this.SandPitStates[f.x, f.y] = SandPitState.Air;
					this.SandPitStates[f.x - 1, f.y + 1] = SandPitState.Sand;
					this.falling = (f.x - 1, f.y + 1);
					return new TickResult(true, (f.x, f.y), this.falling);
				}

				if (f.x >= this.Width - 1)
				{
					// falling out the right side of the pit into the abyss
					this.SandPitStates[f.x, f.y] = SandPitState.Air;
					return new TickResult(false, this.falling, null);
				}

				if (this.SandPitStates[f.x + 1, f.y + 1] == SandPitState.Air)
				{
					// sand flowing to the right
					this.SandPitStates[f.x, f.y] = SandPitState.Air;
					this.SandPitStates[f.x + 1, f.y + 1] = SandPitState.Sand;
					this.falling = (f.x + 1, f.y + 1);
					return new TickResult(true, (f.x, f.y), this.falling);
				}

				// settle
				this.falling = null;
				return new TickResult(true, null, null);

			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	private void Initialize(IEnumerable<RockPath> rockPaths)
	{
		(int x, int y)[] points = rockPaths.SelectMany(r => r.Points).ToArray();

		int minY = Math.Min(points.Min(p => p.y), 0);
		int maxY = points.Max(p => p.y) + 2;
		this.Height = maxY - minY + 1;

		int minX = 500 - this.Height - 10;
		int maxX = 500 + this.Height + 10;

		this.Width = maxX - minX + 1;

		this.SandPitStates = new SandPitState[this.Width, this.Height];

		this.xOffset = minX;

		foreach (RockPath rockPath in rockPaths)
		{
			for (int i = 0; i < rockPath.Points.Length - 1; i++)
			{
				(int x, int y) a = rockPath.Points[i];
				(int x, int y) b = rockPath.Points[i + 1];

				if (a.x == b.x)
				{
					for (int y = Math.Min(a.y, b.y); y <= Math.Max(a.y, b.y); y++)
					{
						this.SandPitStates[a.x - this.xOffset, y] = SandPitState.Rock;
					}
				}
				else if (a.y == b.y)
				{
					for (int x = Math.Min(a.x, b.x); x <= Math.Max(a.x, b.x); x++)
					{
						this.SandPitStates[x - this.xOffset, a.y] = SandPitState.Rock;
					}
				}
				else
				{
					// sanity check
					throw new InvalidOperationException();
				}
			}
		}

		for (int x = 0; x < this.Width; x++)
		{
			this.SandPitStates[x, this.Height - 1] = SandPitState.Rock;
		}
	}
}










