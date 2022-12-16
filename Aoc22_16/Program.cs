namespace Aoc22_16;

using System.Text.RegularExpressions;
using AocCommon;

internal class Program
{
	private static readonly Regex regex = new(@"\d+");

	private static async Task Main(string[] args)
	{
		PipeNetwork pipeNetwork = new();

		await new SingleLineProcessor<PipeNetwork>((line, network) =>
		{
			string[] tokens = line.Split(' ');
			Valve valve = new()
			{
				Id = tokens[1],
				FlowRate = int.Parse(Program.regex.Match(tokens[4]).Value),
				DirectNeighbors = tokens.Skip(9).Select(s => s[..2]).ToArray()
			};
			network!.Valves.Add(valve.Id, valve);
		}).Process(pipeNetwork);

		pipeNetwork.LayPipes();
		pipeNetwork.FindMaxFlowRate();

		Console.WriteLine("DONE");
	}
}






