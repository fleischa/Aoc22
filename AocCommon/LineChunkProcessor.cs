namespace AocCommon;

public class LineChunkProcessor<TState>
{
	private readonly Func<string[], TState?, bool>? cancelableHandler;
	private readonly int chunkSize;
	private readonly Action<string[], TState?>? handler;
	private readonly string inputFile;

	public LineChunkProcessor(Action<string[], TState?> handler, int chunkSize, string inputFile = "input.txt")
	{
		this.handler = handler;
		this.chunkSize = chunkSize;
		this.inputFile = inputFile;
	}

	public LineChunkProcessor(Func<string[], TState?, bool> cancelableHandler, int chunkSize, string inputFile = "input.txt")
	{
		this.cancelableHandler = cancelableHandler;
		this.chunkSize = chunkSize;
		this.inputFile = inputFile;
	}

	public int LinesRead { get; private set; }

	public async Task Process(TState? state = default)
	{
		using StreamReader reader = File.OpenText(this.inputFile);

		List<string> chunk = new();

		while (await reader.ReadLineAsync() is { } line)
		{
			this.LinesRead++;

			chunk.Add(line);

			if (chunk.Count >= this.chunkSize)
			{
				string[] chunkArray = chunk.ToArray();
				chunk.Clear();

				if (this.cancelableHandler != null)
				{
					if (!this.cancelableHandler(chunkArray, state))
					{
						break;
					}
				}
				else if (this.handler != null)
				{
					this.handler(chunkArray, state);
				}
			}
		}

		if (chunk.Any())
		{
			string[] chunkArray = chunk.ToArray();

			if (this.cancelableHandler != null)
			{
				this.cancelableHandler(chunkArray, state);
			}
			else if (this.handler != null)
			{
				this.handler(chunkArray, state);
			}
		}
	}
}
