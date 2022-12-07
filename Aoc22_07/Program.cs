namespace Aoc22_07;

using AocCommon;

internal class Program
{
	private const int updateSize = 30000000;

	private static async Task Main(string[] args)
	{
		ElfFileSystem fileSystem = new();

		await new SingleLineProcessor<ElfFileSystem>((line, fs) =>
		{
			TerminalLine terminalLine = TerminalLine.Parse(line);
			fs!.ApplyTerminalLine(terminalLine);
		}).Process(fileSystem);

		int smallDirectoriesSize = fileSystem.DirectoryCache.Where(d => d.Size <= 100000).Sum(d => d.Size);
		Console.WriteLine($"sum of small directories: {smallDirectoriesSize}");

		int requiredFreeStorage = Program.updateSize - fileSystem.FreeStorage;
		int smallestDirectory = fileSystem.DirectoryCache.Where(d => d.Size >= requiredFreeStorage).Min(d => d.Size);
		Console.WriteLine($"size of smallest directory to delete: {smallestDirectory}");
	}
}
