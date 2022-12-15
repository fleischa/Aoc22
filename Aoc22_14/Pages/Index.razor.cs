namespace Aoc22_14.Pages;

using Blazor.Extensions;
using Blazor.Extensions.Canvas.Canvas2D;
using Microsoft.AspNetCore.Components;

public partial class Index
{
	private const int cellSize = 4;
	private Canvas2DContext canvas2DContext = null!;
	private int canvasHeight;
	private BECanvasComponent canvasRef = null!;
	private int canvasWidth;
	private PeriodicTimer? periodicTimer;
	private SandPit? sandPit;

	[Inject]
	private HttpClient HttpClient { get; set; } = null!;

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		await base.OnAfterRenderAsync(firstRender);

		if (!firstRender)
		{
			return;
		}

		List<RockPath> rockPaths = new();

		using StreamReader reader = new(await this.HttpClient.GetStreamAsync("input.txt"));
		while (await reader.ReadLineAsync() is { } line)
		{
			rockPaths.Add(RockPath.Parse(line));
		}

		this.sandPit = new SandPit(rockPaths);
		this.canvasWidth = this.sandPit.Width * Index.cellSize;
		this.canvasHeight = this.sandPit.Height * Index.cellSize;

		await this.InvokeAsync(this.StateHasChanged);

		this.canvas2DContext = await this.canvasRef.CreateCanvas2DAsync();
		await this.DrawFull();

		double maxFps = 240;
		this.periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1.0 / maxFps));
		while (await this.periodicTimer.WaitForNextTickAsync())
		{
			TickResult tickResult = this.sandPit.Tick();
			await this.DrawDelta(tickResult);

			if (!tickResult.IsRunning)
			{
				break;
			}
		}
	}

	private async Task DrawDelta(TickResult tickResult)
	{
		await this.canvas2DContext.BeginBatchAsync();

		if (tickResult.RemoveSand.HasValue)
		{
			await this.canvas2DContext.SetFillStyleAsync("white");
			await this.canvas2DContext.FillRectAsync(Index.cellSize * tickResult.RemoveSand.Value.x,
													Index.cellSize * tickResult.RemoveSand.Value.y,
													Index.cellSize,
													Index.cellSize);
		}

		if (tickResult.AddSand.HasValue)
		{
			await this.canvas2DContext.SetFillStyleAsync("orange");
			await this.canvas2DContext.FillRectAsync(Index.cellSize * tickResult.AddSand.Value.x,
													Index.cellSize * tickResult.AddSand.Value.y,
													Index.cellSize,
													Index.cellSize);
		}

		await this.canvas2DContext.EndBatchAsync();
	}

	private async Task DrawFull()
	{
		await this.canvas2DContext.BeginBatchAsync();

		await this.canvas2DContext.SetFillStyleAsync("white");
		await this.canvas2DContext.FillRectAsync(0, 0, Index.cellSize * this.sandPit!.Width, Index.cellSize * this.sandPit.Height);

		for (int y = 0; y < this.sandPit!.Height; y++)
		{
			for (int x = 0; x < this.sandPit.Width; x++)
			{
				switch (this.sandPit.SandPitStates[x, y])
				{
					case SandPitState.Rock:
						await this.canvas2DContext.SetFillStyleAsync("gray");
						await this.canvas2DContext.FillRectAsync(Index.cellSize * x, Index.cellSize * y, Index.cellSize, Index.cellSize);
						break;
					case SandPitState.Sand:
						await this.canvas2DContext.SetFillStyleAsync("orange");
						await this.canvas2DContext.FillRectAsync(Index.cellSize * x, Index.cellSize * y, Index.cellSize, Index.cellSize);
						break;
				}
			}
		}

		await this.canvas2DContext.EndBatchAsync();
	}
}







































