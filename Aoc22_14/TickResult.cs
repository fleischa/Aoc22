namespace Aoc22_14;

public class TickResult
{
	public TickResult(bool isRunning, (int x, int y)? removeSand, (int x, int y)? addSand)
	{
		this.IsRunning = isRunning;
		this.RemoveSand = removeSand;
		this.AddSand = addSand;
	}

	public bool IsRunning { get; }

	public (int x, int y)? RemoveSand { get; }

	public (int x, int y)? AddSand { get; }
}

