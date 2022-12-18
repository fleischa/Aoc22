namespace Aoc22_16;

public class ValvePathStep
{
	public Valve Valve { get; set; }

	public int PipeIndex { get; set; }

	public int MinutesLeft { get; set; }

	public int OperationDuration { get; set; }

	public int Reactivation
	{
		get { return this.MinutesLeft + this.OperationDuration; }
	}
}



