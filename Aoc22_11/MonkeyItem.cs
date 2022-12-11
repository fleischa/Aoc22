namespace Aoc22_11;

internal class MonkeyItem
{
	private readonly int initialWorryLevel;
	private Dictionary<int, int> remainders = null!;

	public MonkeyItem(int initialWorryLevel)
	{
		this.initialWorryLevel = initialWorryLevel;
	}

	public void Inspect(MonkeyOperation operation, int parameter)
	{
		foreach (int testModulo in this.remainders.Keys)
		{
			int r = this.remainders[testModulo];

			switch (operation)
			{
				case MonkeyOperation.Add:
					r += parameter;
					break;
				case MonkeyOperation.Multiply:
					r *= parameter;
					break;
				case MonkeyOperation.Square:
					r *= r;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			this.remainders[testModulo] = r % testModulo;
		}
	}

	public bool IsDivisibleBy(int testModulo)
	{
		return this.remainders[testModulo] == 0;
	}

	internal void InitializeRemainders(IEnumerable<int> testModulos)
	{
		this.remainders = testModulos.ToDictionary(m => m, m => this.initialWorryLevel % m);
	}
}







