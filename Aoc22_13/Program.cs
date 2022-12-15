namespace Aoc22_13;

using AocCommon;

internal class Program
{
	private static async Task Main(string[] args)
	{
		List<Packet> packets = new();
		await new SingleLineProcessor<object>((line, _) =>
		{
			if (!string.IsNullOrWhiteSpace(line))
			{
				packets.Add(Packet.Parse(line));
			}
		}).Process();

		int index = 1;
		int sum = 0;
		foreach (Packet[] pair in packets.Chunk(2))
		{
			Packet left = pair[0];
			Packet right = pair[1];

			if (left < right)
			{
				sum += index;
			}

			index++;
		}

		Console.WriteLine($"pairs in right order: {sum}");

		Packet divider1 = Packet.Parse("[[2]]");
		Packet divider2 = Packet.Parse("[[6]]");

		packets.Add(divider1);
		packets.Add(divider2);

		packets.Sort();

		Console.WriteLine($"decoder key: {(packets.IndexOf(divider1) + 1) * (packets.IndexOf(divider2) + 1)}");
	}
}

