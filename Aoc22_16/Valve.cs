namespace Aoc22_16;

public class Valve
{
	public required string Id { get; init; }

	public required string[] DirectNeighbors { get; init; }

	public required int FlowRate { get; init; }

	public Dictionary<string, Pipe> Pipes { get; } = new();

	public void LayPipes(Dictionary<string, Valve> valves)
	{
		foreach (string neighbor in this.DirectNeighbors)
		{
			valves[neighbor].AddPipe(this, valves, 1);
		}
	}

	public void AddPipe(Valve valve, Dictionary<string, Valve> valves, int distance)
	{
		if (this.Pipes.TryGetValue(valve.Id, out Pipe? pipe) && pipe.Distance <= distance)
		{
			// there is already a shorter or equally long path known
			return;
		}

		this.Pipes[valve.Id] = new Pipe
		{
			Valve = valve,
			Distance = distance
		};

		foreach (string neighbor in this.DirectNeighbors)
		{
			valves[neighbor].AddPipe(valve, valves, distance + 1);
		}
	}
}







