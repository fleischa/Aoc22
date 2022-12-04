namespace Aoc22_04;

internal record Assignment : IParsable<Assignment?>
{
	private const char separator = '-';

	public required int Start { get; init; }
	public required int End { get; init; }

	public static Assignment Parse(string s, IFormatProvider? provider = null)
	{
		if (Assignment.TryParse(s, provider, out Assignment? assignment))
		{
			return assignment!;
		}

		throw new ArgumentException();
	}

	public static bool TryParse(string? s, IFormatProvider? provider, out Assignment? result)
	{
		if (string.IsNullOrWhiteSpace(s))
		{
			result = default;
			return false;
		}

		string[] tokens = s.Split(Assignment.separator);

		if (tokens.Length != 2)
		{
			result = default;
			return false;
		}

		if (int.TryParse(tokens[0], out int start) && int.TryParse(tokens[1], out int end))
		{
			result = new Assignment
			{
				Start = start,
				End = end
			};
			return true;
		}

		result = default;
		return false;
	}

	public override string ToString()
	{
		return $"{this.Start}{Assignment.separator}{this.End}";
	}

	public bool Intersects(Assignment other)
	{
		return this.Start <= other.End && this.End >= other.Start;
	}

	public bool Contains(Assignment other)
	{
		return this.Start <= other.Start && this.End >= other.End;
	}
}
