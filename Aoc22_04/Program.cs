namespace Aoc22_04;

using AocCommon;

internal class Program
{
	private static int fullOverlaps;
	private static int overlaps;

	private static async Task Main(string[] args)
	{
		await new SingleLineProcessor<object>((line, _) => Program.FindFullAssignmentOverlaps(line)).Process();
		Console.WriteLine($"full assignment overlaps: {Program.fullOverlaps}");

		await new SingleLineProcessor<object>((line, _) => Program.FindAssignmentOverlaps(line)).Process();
		Console.WriteLine($"assignment overlaps: {Program.overlaps}");
	}

	private static void FindFullAssignmentOverlaps(string line)
	{
		AssignmentGroup assignmentGroup = AssignmentGroup.Parse(line);

		if (assignmentGroup.Assignments.Where((a, i) => assignmentGroup.Assignments.Where((_, j) => j != i).Any(a.Contains)).Any())
		{
			Program.fullOverlaps++;
		}
	}

	private static void FindAssignmentOverlaps(string line)
	{
		AssignmentGroup assignmentGroup = AssignmentGroup.Parse(line);

		if (assignmentGroup.Assignments.Where((a, i) => assignmentGroup.Assignments.Skip(i + 1).Any(a.Intersects)).Any())
		{
			Program.overlaps++;
		}
	}
}
