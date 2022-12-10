namespace Aoc22_10;

using AocCommon;

internal class Program
{
	private static readonly int[] sampleCycles = { 20, 60, 100, 140, 180, 220 };

	private static async Task Main(string[] args)
	{
		ElfCommunicator elfCommunicator = new(Program.sampleCycles);

		await new SingleLineProcessor<ElfCommunicator>((line, c) => { c!.ProcessInstruction(line); }).Process(elfCommunicator);

		Console.WriteLine($"sample sum: {elfCommunicator.SampleSum}");
		Console.WriteLine();
		Console.WriteLine(elfCommunicator);
	}
}
