namespace Aoc22_02;

using AocCommon;

internal class Program
{
	private static int score;

	private static async Task Main(string[] args)
	{
		await new SingleLineProcessor<object>((l, _) => Program.PlayRockPaperScissors(l)).Process();
		Console.WriteLine(Program.score);
	}

	private static void PlayRockPaperScissors(string line)
	{
		string[] tokens = line.Split(' ');

		RpsSymbol opponent = tokens[0] switch
		{
			"A" => RpsSymbol.Rock,
			"B" => RpsSymbol.Paper,
			"C" => RpsSymbol.Scissors,
			_ => throw new ArgumentException()
		};

		GameOutcome outcome = tokens[1] switch
		{
			"X" => GameOutcome.Loss,
			"Y" => GameOutcome.Draw,
			"Z" => GameOutcome.Win,
			_ => throw new ArgumentException()
		};

		RpsSymbol player = opponent switch
		{
			RpsSymbol.Rock => outcome switch
			{
				GameOutcome.Loss => RpsSymbol.Scissors,
				GameOutcome.Draw => RpsSymbol.Rock,
				GameOutcome.Win => RpsSymbol.Paper,
				_ => throw new ArgumentOutOfRangeException()
			},
			RpsSymbol.Paper => outcome switch
			{
				GameOutcome.Loss => RpsSymbol.Rock,
				GameOutcome.Draw => RpsSymbol.Paper,
				GameOutcome.Win => RpsSymbol.Scissors,
				_ => throw new ArgumentOutOfRangeException()
			},
			RpsSymbol.Scissors => outcome switch
			{
				GameOutcome.Loss => RpsSymbol.Paper,
				GameOutcome.Draw => RpsSymbol.Scissors,
				GameOutcome.Win => RpsSymbol.Rock,
				_ => throw new ArgumentOutOfRangeException()
			},
			_ => throw new ArgumentOutOfRangeException()
		};

		Program.score += player switch
		{
			RpsSymbol.Rock => 1,
			RpsSymbol.Paper => 2,
			RpsSymbol.Scissors => 3,
			_ => throw new ArgumentOutOfRangeException()
		};

		Program.score += outcome switch
		{
			GameOutcome.Loss => 0,
			GameOutcome.Draw => 3,
			GameOutcome.Win => 6,
			_ => throw new ArgumentOutOfRangeException()
		};
	}
}
