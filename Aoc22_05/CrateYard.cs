namespace Aoc22_05;

internal class CrateYard : IParsable<CrateYard?>
{
	public required Stack<char>[] Stacks { get; init; }

	public static CrateYard Parse(string s, IFormatProvider? provider = null)
	{
		if (CrateYard.TryParse(s, provider, out CrateYard? crateYard))
		{
			return crateYard!;
		}

		throw new ArgumentException();
	}

	public static bool TryParse(string? s, IFormatProvider? provider, out CrateYard? result)
	{
		if (string.IsNullOrWhiteSpace(s))
		{
			result = default;
			return false;
		}

		try
		{
			using StringReader reader = new(s);
			string header = reader.ReadLine()!.Trim();

			Stack<char>[] stacks = new Stack<char>[header.Length];
			for (int i = 0; i < header.Length; i++)
			{
				stacks[i] = new Stack<char>();
			}

			while (reader.ReadLine() is { } line)
			{
				for (int i = 0; i < stacks.Length && i < line.Length; i++)
				{
					if (char.IsLetter(line[i]))
					{
						stacks[i].Push(line[i]);
					}
				}
			}

			result = new CrateYard
			{
				Stacks = stacks
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

	public void ApplyCrateMover9000Instruction(CraneInstruction instruction)
	{
		for (int i = 0; i < instruction.Count; i++)
		{
			this.Stacks[instruction.To].Push(this.Stacks[instruction.From].Pop());
		}
	}

	public void ApplyCrateMover9001Instruction(CraneInstruction instruction)
	{
		Stack<char> bufferStack = new();

		for (int i = 0; i < instruction.Count; i++)
		{
			bufferStack.Push(this.Stacks[instruction.From].Pop());
		}

		while (bufferStack.Any())
		{
			this.Stacks[instruction.To].Push(bufferStack.Pop());
		}
	}

	public string GetTopLayer()
	{
		return new string(this.Stacks.Select(s => s.Any() ? s.Peek() : ' ').ToArray());
	}
}
