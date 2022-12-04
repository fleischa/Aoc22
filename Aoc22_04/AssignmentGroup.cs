namespace Aoc22_04;

internal record AssignmentGroup : IParsable<AssignmentGroup?>
{
	private const char separator = ',';

	public required Assignment[] Assignments { get; init; }

	public static AssignmentGroup Parse(string s, IFormatProvider? provider = null)
	{
		if (AssignmentGroup.TryParse(s, provider, out AssignmentGroup? assignmentPair))
		{
			return assignmentPair!;
		}

		throw new ArgumentException();
	}

	public static bool TryParse(string? s, IFormatProvider? provider, out AssignmentGroup? result)
	{
		if (string.IsNullOrWhiteSpace(s))
		{
			result = default;
			return false;
		}

		string[] tokens = s.Split(AssignmentGroup.separator);

		try
		{
			result = new AssignmentGroup
			{
				Assignments = tokens.Select(t => Assignment.Parse(t)).ToArray()
			};
			return true;
		}
		catch
		{
			// If parsing of one Assignment fails, parsing of the AssignmentGroup fails.
			// Since TryParse should not throw an exception, we catch that here and return gracefully.
		}

		result = default;
		return false;
	}

	public override string ToString()
	{
		return string.Join(AssignmentGroup.separator, this.Assignments.AsEnumerable());
	}
}
