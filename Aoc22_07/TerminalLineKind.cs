namespace Aoc22_07;

internal enum TerminalLineKind
{
	None,
	CmdCdRoot,
	CmdCdUp,
	CmdCd,
	CmdList,
	OutDir,
	OutFile
}
