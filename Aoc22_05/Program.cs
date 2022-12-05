namespace Aoc22_05;

using AocCommon;

internal class Program
{
	private static async Task Main(string[] args)
	{
		using StreamReader reader = File.OpenText("init.txt");
		string s = await reader.ReadToEndAsync();

		CrateYard crateYard9000 = CrateYard.Parse(s);
		await new SingleLineProcessor<CrateYard>(Program.ApplyCrateMover9000Instructions!).Process(crateYard9000);
		Console.WriteLine(crateYard9000.GetTopLayer());

		CrateYard crateYard9001 = CrateYard.Parse(s);
		await new SingleLineProcessor<CrateYard>(Program.ApplyCrateMover9001Instructions!).Process(crateYard9001);
		Console.WriteLine(crateYard9001.GetTopLayer());
	}

	private static void ApplyCrateMover9000Instructions(string line, CrateYard crateYard)
	{
		CraneInstruction instuction = CraneInstruction.Parse(line);
		crateYard.ApplyCrateMover9000Instruction(instuction);
	}

	private static void ApplyCrateMover9001Instructions(string line, CrateYard crateYard)
	{
		CraneInstruction instuction = CraneInstruction.Parse(line);
		crateYard.ApplyCrateMover9001Instruction(instuction);
	}
}
