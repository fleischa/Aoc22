namespace Aoc22_16;

public class PipeNetwork
{
	private static int maxFlowRate;

	public Dictionary<string, Valve> Valves { get; } = new();

	public void LayPipes()
	{
		foreach (Valve valve in this.Valves.Values.Where(v => v.FlowRate > 0))
		{
			valve.LayPipes(this.Valves);
		}
	}

	public void FindMaxFlowRate(int numberOfAgents, int minutesLeft)
	{
		Valve startValve = this.Valves["AA"];

		ValveOpeningAgent[] agents = Enumerable.Range(0, numberOfAgents)
		.Select(a => new ValveOpeningAgent(a,
											new ValvePathStep
											{
												Valve = startValve,
												MinutesLeft = minutesLeft
											}))
		.ToArray();

		int flowRate = 0;

		while (true)
		{
			ValveOpeningAgent agent = agents.OrderBy(a => a.ValvePath.Peek().Reactivation).ThenBy(a => a.Id).First();
			// TODO
		}
	}

	public void FindMaxFlowRate()
	{
		Valve startValve = this.Valves["AA"];
		Stack<string> openedValves = new();
		PipeNetwork.Solve(startValve, openedValves, 30, 0);
	}

	private static void Solve(Valve valve, Stack<string> openedValves, int minutesLeft, int flowRate)
	{
		List<Pipe> pipes = valve.Pipes.Values.Where(p => !openedValves.Contains(p.Valve.Id)).ToList();

		if (!pipes.Any())
		{
			if (flowRate > PipeNetwork.maxFlowRate)
			{
				PipeNetwork.maxFlowRate = flowRate;
				Console.WriteLine($"new max flow rate: {flowRate}");
				return;
			}
		}

		foreach (Pipe pipe in valve.Pipes.Values)
		{
			pipe.CalculateValue(minutesLeft);
		}

		pipes = pipes.Where(p => p.Value > 0).ToList();

		if (!pipes.Any())
		{
			if (flowRate > PipeNetwork.maxFlowRate)
			{
				PipeNetwork.maxFlowRate = flowRate;
				Console.WriteLine($"new max flow rate: {flowRate}");
				return;
			}
		}

		foreach (Pipe pipe in pipes.OrderByDescending(p => p.Value))
		{
			openedValves.Push(pipe.Valve.Id);
			PipeNetwork.Solve(pipe.Valve, openedValves, minutesLeft - pipe.Distance - 1, flowRate + pipe.Value);
			openedValves.Pop();
		}
	}
}

















