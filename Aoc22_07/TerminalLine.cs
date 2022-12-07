namespace Aoc22_07;

internal record TerminalLine : IParsable<TerminalLine?>
{
	public TerminalLineKind Kind { get; init; }

	public string Name { get; init; } = string.Empty;

	public int Size { get; init; }

	public static TerminalLine Parse(string s, IFormatProvider? provider = null)
	{
		if (TerminalLine.TryParse(s, provider, out TerminalLine? terminalLine))
		{
			return terminalLine!;
		}

		throw new ArgumentException();
	}

	public static bool TryParse(string? s, IFormatProvider? provider, out TerminalLine? result)
	{
		if (string.IsNullOrWhiteSpace(s))
		{
			result = default;
			return false;
		}

		string[] tokens = s.Split(' ');

		if (tokens.Length < 2)
		{
			result = default;
			return false;
		}

		TerminalLineKind kind = TerminalLineKind.None;
		string name = string.Empty;
		int size = 0;

		try
		{
			switch (tokens[0])
			{
				case "$":
					switch (tokens[1])
					{
						case "cd":
							switch (tokens[2])
							{
								case "/":
									kind = TerminalLineKind.CmdCdRoot;
									break;
								case "..":
									kind = TerminalLineKind.CmdCdUp;
									break;
								default:
									kind = TerminalLineKind.CmdCd;
									name = tokens[2];
									break;
							}

							break;
						case "ls":
							kind = TerminalLineKind.CmdList;
							break;
					}

					break;
				case "dir":
					kind = TerminalLineKind.OutDir;
					name = tokens[1];
					break;
				default:
					kind = TerminalLineKind.OutFile;
					size = int.Parse(tokens[0]);
					name = tokens[1];
					break;
			}

			result = new TerminalLine
			{
				Kind = kind,
				Name = name,
				Size = size
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
}
