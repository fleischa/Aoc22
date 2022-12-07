namespace Aoc22_07;

internal class ElfFileSystem
{
	public const int MaxStorage = 70000000;

	private readonly Stack<ElfDirectory> currentPath = new();

	public readonly List<ElfDirectory> DirectoryCache = new();

	public ElfDirectory Root { get; } = new()
	{
		Name = "/"
	};

	public ElfDirectory CurrentDirectory
	{
		get
		{
			if (this.currentPath.Any())
			{
				return this.currentPath.Peek();
			}

			return this.Root;
		}
	}

	public int FreeStorage
	{
		get { return ElfFileSystem.MaxStorage - this.Root.Size; }
	}

	public void CdRoot()
	{
		this.currentPath.Clear();
	}

	public void CdUp()
	{
		this.currentPath.Pop();
	}

	public void Cd(string name)
	{
		if (!this.CurrentDirectory.ChildDirectories.TryGetValue(name, out ElfDirectory? d))
		{
			throw new ArgumentException();
		}

		this.currentPath.Push(d);
	}

	public void Add(ElfDirectory directory)
	{
		if (!this.CurrentDirectory.ChildDirectories.ContainsKey(directory.Name))
		{
			this.DirectoryCache.Add(directory);
			this.CurrentDirectory.ChildDirectories[directory.Name] = directory;
		}
	}

	public void Add(ElfFile file)
	{
		this.CurrentDirectory.Files[file.Name] = file;
	}

	public void ApplyTerminalLine(TerminalLine terminalLine)
	{
		switch (terminalLine.Kind)
		{
			case TerminalLineKind.CmdCdRoot:
				this.CdRoot();
				break;
			case TerminalLineKind.CmdCdUp:
				this.CdUp();
				break;
			case TerminalLineKind.CmdCd:
				this.Cd(terminalLine.Name);
				break;
			case TerminalLineKind.OutDir:
				this.Add(new ElfDirectory
				{
					Name = terminalLine.Name
				});
				break;
			case TerminalLineKind.OutFile:
				this.Add(new ElfFile
				{
					Name = terminalLine.Name,
					Size = terminalLine.Size
				});
				break;
			case TerminalLineKind.CmdList:
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}
}
