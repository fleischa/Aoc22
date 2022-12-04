namespace AocCommon;

public class SingleLineProcessor<TState>
{
	private readonly Func<string, TState?, bool>? cancelableHandler;
	private readonly Action<string, TState?>? handler;
	private readonly string inputFile;

	public SingleLineProcessor(Action<string, TState?> handler, string inputFile = "input.txt")
	{
		this.handler = handler;
		this.inputFile = inputFile;
	}

	public SingleLineProcessor(Func<string, TState?, bool> cancelableHandler, string inputFile = "input.txt")
	{
		this.cancelableHandler = cancelableHandler;
		this.inputFile = inputFile;
	}

	public int LinesRead { get; private set; }

	public async Task Process(TState? state = default)
	{
		using StreamReader reader = File.OpenText(this.inputFile);

		if (this.cancelableHandler != null)
		{
			while (await reader.ReadLineAsync() is { } line)
			{
				this.LinesRead++;

				if (!this.cancelableHandler(line, state))
				{
					break;
				}
			}
		}
		else if (this.handler != null)
		{
			while (await reader.ReadLineAsync() is { } line)
			{
				this.LinesRead++;

				this.handler(line, state);
			}
		}
	}
}
