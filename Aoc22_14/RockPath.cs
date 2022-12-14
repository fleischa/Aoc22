namespace Aoc22_14;

public class RockPath : IParsable<RockPath?>
{
	public (int x, int y)[] Points { get; private init; } = null!;

	public static RockPath Parse(string s, IFormatProvider? provider = null)
	{
		if (RockPath.TryParse(s, provider, out RockPath? result))
		{
			return result!;
		}

		throw new ArgumentException();
	}

	public static bool TryParse(string? s, IFormatProvider? provider, out RockPath? result)
	{
		if (string.IsNullOrWhiteSpace(s))
		{
			result = default;
			return false;
		}

		result = new RockPath
		{
			Points = s.Split(' ').Where((_, i) => i % 2 == 0).Select(RockPath.ParsePoint).ToArray()
		};
		return true;
	}

	private static (int x, int y) ParsePoint(string p)
	{
		int[] values = p.Split(',').Select(int.Parse).ToArray();
		return (values[0], values[1]);
	}
}










