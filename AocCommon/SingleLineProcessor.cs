namespace AocCommon;

public class SingleLineProcessor<TState>
{
	private readonly Func<string, TState?, bool>? cancelableHandler;
	private readonly Action<string, TState?>? handler;
	private readonly string inputFile;

	public SingleLineProcessor(Action<string, TState?> handler, string inputFile = "input")
	{
		this.handler = handler;
		this.inputFile = inputFile;
	}

	public SingleLineProcessor(Func<string, TState?, bool> cancelableHandler, string inputFile = "input")
	{
		this.cancelableHandler = cancelableHandler;
		this.inputFile = inputFile;
	}

	public async Task Process(TState? state = default)
	{
		using StreamReader reader = File.OpenText(this.inputFile);

		if (this.cancelableHandler != null)
		{
			while (await reader.ReadLineAsync() is { } line)
			{
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
				this.handler(line, state);
			}
		}
	}
}
