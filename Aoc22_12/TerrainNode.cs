namespace Aoc22_12;

public class TerrainNode
{
	public TerrainNode(int height)
	{
		this.Height = height;
	}

	public List<TerrainNode> Neighbors { get; } = new();

	public int Height { get; set; }

	public bool IsMainStart { get; set; }
	
	public bool IsEnd { get; set; }

	public int PathLength { get; set; } = int.MaxValue;

	public void Start()
	{
		this.PathLength = 0;

		foreach (TerrainNode neighbor in this.Neighbors)
		{
			neighbor.Visit(this);
		}
	}

	public void Visit(TerrainNode origin)
	{
		if (this.Height > origin.Height + 1 || this.PathLength - 1 <= origin.PathLength)
		{
			return;
		}

		this.PathLength = origin.PathLength + 1;

		if (this.IsEnd)
		{
			return;
		}

		foreach (TerrainNode neighbor in this.Neighbors.Where(n => n != origin))
		{
			neighbor.Visit(this);
		}
	}
}













