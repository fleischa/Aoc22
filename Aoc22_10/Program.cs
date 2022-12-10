namespace Aoc22_10;

using AocCommon;

internal class Program
{
	private static readonly int[] samples = { 20, 60, 100, 140, 180, 220 };

	private static async Task Main(string[] args)
	{
		int regX = 1;
		int cycle = 0;

		await new SingleLineProcessor<object>((line, _) =>
		{
			string[] tokens = line.Split(' ');

			cycle++;

			if (tokens[0] == "addx")
			{
				cycle++;
				regX += int.Parse(tokens[1]);
			}
		}).Process();
	}
}
