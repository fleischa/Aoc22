namespace Aoc22_17;

internal class Program
{
	private static async Task Main(string[] args)
	{
		Rock[] rocks =
		{
			// ####
			new(4, 1, (0, 0, RockShape.Rock), (1, 0, RockShape.Rock), (2, 0, RockShape.Rock), (3, 0, RockShape.Rock)),

			// .#.
			// ###
			// .#.
			new(3, 3, (1, 0, RockShape.Rock), (0, 1, RockShape.Rock), (1, 1, RockShape.Rock), (2, 1, RockShape.Rock), (1, 2, RockShape.Rock)),

			// ..#
			// ..#
			// ###
			new(3, 3, (0, 0, RockShape.Rock), (1, 0, RockShape.Rock), (2, 0, RockShape.Rock), (2, 1, RockShape.Rock), (2, 2, RockShape.Rock)),

			// #
			// #
			// #
			// #
			new(1, 4, (0, 0, RockShape.Rock), (0, 1, RockShape.Rock), (0, 2, RockShape.Rock), (0, 3, RockShape.Rock)),

			// ##
			// ##
			new(2, 2, (0, 0, RockShape.Rock), (1, 0, RockShape.Rock), (0, 1, RockShape.Rock), (1, 1, RockShape.Rock))
		};

		using StreamReader reader = File.OpenText("input.txt");
		string jetsString = await reader.ReadToEndAsync();
		int[] jets = jetsString.Where(c => c is '<' or '>')
		.Select(j =>
			{
				switch (j)
				{
					case '<':
						return -1;
					case '>':
						return 1;
					default:
						throw new ArgumentOutOfRangeException();
				}
			})
		.ToArray();

		int numberOfRocks = 2022;

		Pit pit = new(rocks, jets);
		pit.Simulate(numberOfRocks);

		Console.WriteLine($"tower height after {numberOfRocks}: {pit.TowerHeight}");
	}
}






