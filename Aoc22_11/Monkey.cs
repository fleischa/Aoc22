namespace Aoc22_11;

internal class Monkey
{
	public Monkey(IEnumerable<int> startingItems,
		MonkeyOperation operation,
		int parameter,
		int testModulo,
		int targetMonkeyOnTestSuccess,
		int targetMonkeyOnTestFailure)
	{
		this.Items = new Queue<MonkeyItem>(startingItems.Select(i => new MonkeyItem(i)));
		this.Operation = operation;
		this.Parameter = parameter;
		this.TestModulo = testModulo;
		this.TargetMonkeyOnTestSuccess = targetMonkeyOnTestSuccess;
		this.TargetMonkeyOnTestFailure = targetMonkeyOnTestFailure;
	}

	public Queue<MonkeyItem> Items { get; }

	public MonkeyOperation Operation { get; }

	public int Parameter { get; }

	public int TestModulo { get; }

	public int TargetMonkeyOnTestSuccess { get; }

	public int TargetMonkeyOnTestFailure { get; }

	public long Activity { get; set; }
}



