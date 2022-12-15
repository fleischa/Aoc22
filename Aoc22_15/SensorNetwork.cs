namespace Aoc22_15;

public class SensorNetwork
{
	public List<Sensor> Sensors { get; } = new();

	public IEnumerable<(int a, int b)> GetConverage(int y, int minX = int.MinValue, int maxX = int.MaxValue)
	{
		IEnumerable<(int a, int b)> coverageRanges = Enumerable.Empty<(int a, int b)>();

		foreach (Sensor sensor in this.Sensors)
		{
			(int a, int b)? c = sensor.GetCoverage(y, minX, maxX);
			if (c.HasValue)
			{
				coverageRanges = SensorNetwork.MergeRange(coverageRanges, c.Value);
			}
		}

		return coverageRanges;
	}

	private static IEnumerable<(int a, int b)> MergeRange(IEnumerable<(int a, int b)> ranges, (int a, int b) r)
	{
		foreach ((int a, int b) range in ranges)
		{
			if (range.a > r.b + 1 || range.b < r.a - 1)
			{
				// no overlap
				yield return range;
			}
			else
			{
				// merge ranges
				r = (Math.Min(range.a, r.a), Math.Max(range.b, r.b));
			}
		}

		yield return r;
	}
}



