namespace Aoc22_14.Pages;

using Microsoft.AspNetCore.Components;

public partial class Index
{
	private SandPit? sandPit;

	[Inject]
	private HttpClient HttpClient { get; set; } = null!;

	protected override async Task OnInitializedAsync()
	{
		await base.OnInitializedAsync();

		List<RockPath> rockPaths = new();

		using StreamReader reader = new(await this.HttpClient.GetStreamAsync("input.txt"));
		while (await reader.ReadLineAsync() is { } line)
		{
			rockPaths.Add(RockPath.Parse(line));
		}

		this.sandPit = new SandPit(rockPaths);

		double fps = 60;
		while (this.sandPit.Tick())
		{
			await Task.Delay(TimeSpan.FromSeconds(1.0 / fps));
			this.StateHasChanged();
		}

		//while (this.sandPit.Tick()) { }
		
		this.StateHasChanged();

		int sand = 0;
		for (int y = 0; y < this.sandPit.Height; y++)
		{
			for (int x = 0; x < this.sandPit.Width; x++)
			{
				if (this.sandPit.SandPitStates[x, y] == SandPitState.Sand)
				{
					sand++;
				}
			}	
		}

		Console.WriteLine(sand);
	}

	private string GetCellKey(int x, int y)
	{
		return $"{x}:{y}";
	}
}






