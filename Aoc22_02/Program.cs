namespace Aoc22_02;

using AocCommon;

internal class Program
{
	private static readonly GameOutcome[][] rpsRules;

	private static int faultyScore;
	private static int revisedScore;

	static Program()
	{
		Program.rpsRules = new[]
		{
			new[] { GameOutcome.Draw, GameOutcome.Win, GameOutcome.Loss },
			new[] { GameOutcome.Loss, GameOutcome.Draw, GameOutcome.Win },
			new[] { GameOutcome.Win, GameOutcome.Loss, GameOutcome.Draw }
		};
	}

	private static async Task Main(string[] args)
	{
		await new SingleLineProcessor<object>((line, _) => Program.PlayFaultyStrategy(line)).Process();
		Console.WriteLine($"faulty score: {Program.faultyScore}");

		await new SingleLineProcessor<object>((line, _) => Program.PlayRevisedStrategy(line)).Process();
		Console.WriteLine($"revised score: {Program.revisedScore}");
	}

	private static void PlayFaultyStrategy(string line)
	{
		RpsStrategy strategy = RpsStrategy.Parse(line);

		GameOutcome outcome = Program.rpsRules[(int)strategy.OpponentMove][(int)strategy.PlayerMove];
		Program.faultyScore += Program.GetScore(strategy.PlayerMove, outcome);
	}

	private static void PlayRevisedStrategy(string line)
	{
		RpsStrategy strategy = RpsStrategy.Parse(line);

		RpsMove playerMove = (RpsMove)Array.IndexOf(Program.rpsRules[(int)strategy.OpponentMove], strategy.DesiredOutcome);
		Program.revisedScore += Program.GetScore(playerMove, strategy.DesiredOutcome);
	}

	private static int GetScore(RpsMove playerMove, GameOutcome outcome)
	{
		int playerMoveScore = playerMove switch
							{
								RpsMove.Rock => 1,
								RpsMove.Paper => 2,
								RpsMove.Scissors => 3,
								_ => throw new ArgumentOutOfRangeException(nameof(playerMove))
							};

		int outcomeScore = outcome switch
							{
								GameOutcome.Loss => 0,
								GameOutcome.Draw => 3,
								GameOutcome.Win => 6,
								_ => throw new ArgumentOutOfRangeException(nameof(outcome))
							};

		return playerMoveScore + outcomeScore;
	}
}
