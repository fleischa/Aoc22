namespace Aoc22_15;

public class Sensor
{
	public Sensor((int x, int y) location, (int x, int y) closestBeacon)
	{
		this.Location = location;
		this.ClosestBeacon = closestBeacon;
		this.Range = Math.Abs(this.ClosestBeacon.x - this.Location.x) + Math.Abs(this.ClosestBeacon.y - this.Location.y);
	}

	public (int x, int y) Location { get; }

	public (int x, int y) ClosestBeacon { get; }

	public int Range { get; }

	public (int a, int b)? GetCoverage(int y, int minX = int.MinValue, int maxX = int.MaxValue)
	{
		int d = this.Range - Math.Abs(y - this.Location.y);

		if (d <= 0)
		{
			return null;
		}

		int a = Math.Max(this.Location.x - d, minX);
		int b = Math.Min(this.Location.x + d, maxX);
		return (a, b);
	}
}







