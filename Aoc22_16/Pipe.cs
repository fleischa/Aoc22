namespace Aoc22_16;

public class Pipe
{
	public required Valve Valve { get; init; }

	public required int Distance { get; init; }

	public int Value { get; private set; }

	public void CalculateValue(int minutesLeft)
	{
		int d = minutesLeft - this.Distance - 1;
		this.Value = Math.Max(this.Valve.FlowRate * d, 0);
	}
}




