namespace Aoc22_07;

internal record ElfDirectory
{
	public required string Name { get; init; }

	public Dictionary<string, ElfDirectory> ChildDirectories { get; } = new();

	public Dictionary<string, ElfFile> Files { get; } = new();

	public int Size
	{
		get { return this.ChildDirectories.Values.Sum(d => d.Size) + this.Files.Values.Sum(f => f.Size); }
	}
}
