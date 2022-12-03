﻿namespace Aoc22_03;

using AocCommon;

internal class Program
{
	private static int duplicateSum;
	private static int badgeSum;

	private static async Task Main(string[] args)
	{
		await new SingleLineProcessor<object>((line, _) => Program.FindDuplicates(line)).Process();
		Console.WriteLine($"Duplicate Priority Sum: {Program.duplicateSum}");

		await new LineChunkProcessor<object>((lines, _) => Program.FindBadge(lines), 3).Process();
		Console.WriteLine($"Badge Priority Sum: {Program.badgeSum}");
	}

	private static void FindDuplicates(string line)
	{
		Rucksack rucksack = Rucksack.Parse(line, null);

		Program.duplicateSum += rucksack.Items.Distinct()
			.Where(i => rucksack.Compartments.All(c => c.Contains(i)))
			.Sum();
	}

	private static void FindBadge(string[] lines)
	{
		Rucksack[] rucksacks = lines.Select(r => Rucksack.Parse(r, null)).ToArray();

		Program.badgeSum += rucksacks.SelectMany(r => r.Items).Distinct()
			.Where(i => rucksacks.All(r => r.Items.Contains(i)))
			.Sum();
	}
}
