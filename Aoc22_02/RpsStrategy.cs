namespace Aoc22_02;

internal record RpsStrategy : IParsable<RpsStrategy?>
{
	private const char separator = ' ';

	public required RpsMove OpponentMove { get; init; }
	public required RpsMove PlayerMove { get; init; }
	public required GameOutcome DesiredOutcome { get; init; }

	public static RpsStrategy Parse(string s, IFormatProvider? provider = null)
	{
		if (RpsStrategy.TryParse(s, provider, out RpsStrategy? strategy))
		{
			return strategy!;
		}

		throw new ArgumentException();
	}

	public static bool TryParse(string? s, IFormatProvider? provider, out RpsStrategy? result)
	{
		if (string.IsNullOrWhiteSpace(s))
		{
			result = default;
			return false;
		}

		string[] tokens = s.Split(RpsStrategy.separator);

		if (tokens.Length != 2)
		{
			result = default;
			return false;
		}

		try
		{
			RpsMove opponentMove = tokens[0] switch
									{
										"A" => RpsMove.Rock,
										"B" => RpsMove.Paper,
										"C" => RpsMove.Scissors,
										_ => throw new ArgumentException()
									};

			RpsMove playerMove = tokens[1] switch
								{
									"X" => RpsMove.Rock,
									"Y" => RpsMove.Paper,
									"Z" => RpsMove.Scissors,
									_ => throw new ArgumentException()
								};

			GameOutcome desiredOutcome = tokens[1] switch
										{
											"X" => GameOutcome.Loss,
											"Y" => GameOutcome.Draw,
											"Z" => GameOutcome.Win,
											_ => throw new ArgumentException()
										};

			result = new RpsStrategy
			{
				OpponentMove = opponentMove,
				PlayerMove = playerMove,
				DesiredOutcome = desiredOutcome
			};
			return true;
		}
		catch
		{
			// TryParse should not throw exceptions so we catch them here and return gracefully
		}

		result = default;
		return false;
	}
}
