namespace Aoc22_17;

public class Rock
{
	public Rock(int width, int height, params (int x, int y, RockShape shape)[] shapes)
	{
		this.Width = width;
		this.Height = height;
		this.Shapes = new RockShape[this.Width, this.Height];

		foreach ((int x, int y, RockShape s) shape in shapes)
		{
			this.Shapes[shape.x, shape.y] = shape.s;
		}
	}

	public RockShape[,] Shapes { get; }

	public int Width { get; }

	public int Height { get; }
}



