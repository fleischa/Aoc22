namespace Aoc22_10;

internal class ElfCommunicator
{
	public const int ScreenWidth = 40;
	public const int ScreenHeight = 6;

	public ElfCommunicator(IEnumerable<int> sampleCycles)
	{
		this.RegX = 1;
		this.SampleCycles = sampleCycles;
	}

	public bool[] ScreenBuffer { get; } = new bool[ElfCommunicator.ScreenWidth * ElfCommunicator.ScreenHeight];

	public IEnumerable<int> SampleCycles { get; }

	public int SampleSum { get; private set; }

	public int Cycle { get; private set; }

	public int RegX { get; private set; }

	public void ProcessInstruction(string instruction)
	{
		string[] tokens = instruction.Split(' ');

		this.IncrementCycle();

		if (tokens[0] == "addx")
		{
			this.IncrementCycle();
			this.RegX += int.Parse(tokens[1]);
		}
	}

	private void IncrementCycle()
	{
		int h = this.Cycle % ElfCommunicator.ScreenWidth;
		this.ScreenBuffer[this.Cycle] = this.RegX >= h - 1 && this.RegX <= h + 1;

		this.Cycle++;

		if (this.SampleCycles.Any(s => s == this.Cycle))
		{
			this.SampleSum += this.Cycle * this.RegX;
		}
	}

	public override string ToString()
	{
		return string.Join('\n', this.ScreenBuffer.Select(b => b ? '#' : ' ').Chunk(ElfCommunicator.ScreenWidth).Select(c => new string(c)));
	}
}
