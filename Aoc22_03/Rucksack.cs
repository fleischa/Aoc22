namespace Aoc22_03;

internal record Rucksack : IParsable<Rucksack?>
{
	private const int numberOfCompartments = 2;

	// Yeah, yeah, I know these relationships are obvious for latin characters. Just in case the Elfs switch to Sanskrit or something...
	private const int lowerOffset = 'a' - 1;
	private const int upperOffset = 'A' - 27;

	public required int[][] Compartments { get; init; }

	public IEnumerable<int> AllItems
	{
		get { return this.Compartments.SelectMany(c => c); }
	}

	public static Rucksack Parse(string s, IFormatProvider? provider = null)
	{
		if (Rucksack.TryParse(s, provider, out Rucksack? rucksack))
		{
			return rucksack!;
		}

		throw new ArgumentException();
	}

	public static bool TryParse(string? s, IFormatProvider? provider, out Rucksack? result)
	{
		// we enforce an equal distribution here, mainly as an input sanity check
		if (s == null || s.Length % Rucksack.numberOfCompartments != 0)
		{
			result = default;
			return false;
		}

		int compartmentSize = s.Length / Rucksack.numberOfCompartments;
		int[] items = s.Select(Rucksack.ToPriority).ToArray();

		result = new Rucksack
		{
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

		// can characters even be neither upper nor lower?
		throw new ArgumentException();
	}
}
