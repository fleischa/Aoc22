namespace Aoc22_16;

public class ValveOpeningAgent
{
	public ValveOpeningAgent(int id, ValvePathStep initialValvePathStep)
	{
		this.Id = id;
		this.ValvePath = new Stack<ValvePathStep>(new[] { initialValvePathStep });
	}

	public int Id { get; }

	public Stack<ValvePathStep> ValvePath { get; }
}



