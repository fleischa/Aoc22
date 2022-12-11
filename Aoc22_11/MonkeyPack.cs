namespace Aoc22_11;

internal class MonkeyPack
{
	public MonkeyPack(params Monkey[] monkeys)
	{
		this.Monkeys = monkeys;

		int[] testModulos = this.Monkeys.Select(m => m.TestModulo).Distinct().ToArray();
		foreach (MonkeyItem item in this.Monkeys.SelectMany(m => m.Items))
		{
			item.InitializeRemainders(testModulos);
		}
	}

	public Monkey[] Monkeys { get; }

	public long MonkeyBusiness
	{
		get
		{
			Monkey[] mostActive = this.Monkeys.OrderByDescending(m => m.Activity).ToArray();
			return mostActive[0].Activity * mostActive[1].Activity;
		}
	}

	private void ProcessMonkey(Monkey monkey)
	{
		while (monkey.Items.TryDequeue(out MonkeyItem? item))
		{
			monkey.Activity++;
			item.Inspect(monkey.Operation, monkey.Parameter);
			int targetMonkeyIndex = item.IsDivisibleBy(monkey.TestModulo) ? monkey.TargetMonkeyOnTestSuccess : monkey.TargetMonkeyOnTestFailure;
			Monkey targetMonkey = this.Monkeys[targetMonkeyIndex];
			targetMonkey.Items.Enqueue(item);
		}
	}

	public void ProcessRounds(int numberOfRounds)
	{
		for (int i = 0; i < numberOfRounds; i++)
		{
			foreach (Monkey monkey in this.Monkeys)
			{
				this.ProcessMonkey(monkey);
			}
		}
	}
}






