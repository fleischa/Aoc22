namespace Aoc22_03;

internal class Rucksack : IParsable<Rucksack?>
{
	private const int numberOfCompartments = 2;
	private const int lowerOffset = 'a' - 1;
	private const int upperOffset = 'A' - 27;

	public required int[] Items { get; init; }
	public required int[][] Compartments { get; init; }

	public static Rucksack Parse(string s, IFormatProvider? provider)
	{
		if (Rucksack.TryParse(s, provider, out Rucksack? rucksack))
		{
			return rucksack!;
		}

		throw new ArgumentException();
	}

	public static bool TryParse(string? s, IFormatProvider? provider, out Rucksack? result)
	{
		if (s == null || s.Length % Rucksack.numberOfCompartments != 0)
		{
			result = default;
			return false;
		}

		int compartmentSize = s.Length / Rucksack.numberOfCompartments;
		int[] items = s.Select(Rucksack.ToPriority).ToArray();

		result = new Rucksack
		{
			Items = items,
			Compartments = items.Chunk(compartmentSize).ToArray()
		};

		return true;
	}

	private static int ToPriority(char item)
	{
		if (char.IsLower(item))
		{
			return item - Rucksack.lowerOffset;
		}

		if (char.IsUpper(item))
		{
			return item - Rucksack.upperOffset;
		}

		throw new ArgumentException();
	}
}
