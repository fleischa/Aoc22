namespace Aoc22_05;

internal record CraneInstruction : IParsable<CraneInstruction?>
{
	public required int Count { get; init; }

	public required int From { get; init; }

	public required int To { get; init; }

	public static CraneInstruction Parse(string s, IFormatProvider? provider = null)
	{
		if (CraneInstruction.TryParse(s, provider, out CraneInstruction? craneInstruction))
		{
			return craneInstruction!;
		}

		throw new ArgumentException();
	}

	public static bool TryParse(string? s, IFormatProvider? provider, out CraneInstruction? result)
	{
		if (string.IsNullOrWhiteSpace(s))
		{
			result = default;
			return false;
		}

		string[] tokens = s.Split(' ');

		if (tokens.Length != 6)
		{
			result = default;
			return false;
		}

		try
		{
			result = new CraneInstruction
			{
				Count = int.Parse(tokens[1]),
				From = int.Parse(tokens[3]) - 1,
				To = int.Parse(tokens[5]) - 1
			};
			return true;
		}
		catch
		{
			// Since TryParse should not throw an exception, we catch that here and return gracefully.
		}

		result = default;
		return false;
	}
}
