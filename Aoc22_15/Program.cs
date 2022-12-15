namespace Aoc22_15;

using System.Text.RegularExpressions;
using AocCommon;

internal class Program
{
	private static readonly Regex regex = new(@"-?\d+");

	private static async Task Main(string[] args)
	{
		SensorNetwork sensorNetwork = new();

		await new SingleLineProcessor<SensorNetwork>((line, network) =>
		{
			MatchCollection matches = Program.regex.Matches(line);
			if (matches.Count != 4)
			{
				throw new ArgumentException();
			}

			network!.Sensors.Add(new Sensor((int.Parse(matches[0].Value), int.Parse(matches[1].Value)),
											(int.Parse(matches[2].Value), int.Parse(matches[3].Value))));
		}).Process(sensorNetwork);

		Program.Part1(sensorNetwork);
		Program.Part2(sensorNetwork);
	}

	private static void Part1(SensorNetwork sensorNetwork)
	{
		int y = 2000000;

		List<(int a, int b)> coverageRanges = sensorNetwork.GetConverage(y).ToList();
		int coverage = coverageRanges.Select(r => r.b - r.a + 1).Sum();

		List<(int x, int y)> beacons = sensorNetwork.Sensors.Where(s => s.ClosestBeacon.y == y).Select(s => s.ClosestBeacon).Distinct().ToList();
		int coveredBeacons = beacons.Count(b => coverageRanges.Any(r => r.a <= b.x && r.b >= b.x));

		Console.WriteLine($"coverage on y={y}: {coverage - coveredBeacons}");
	}

	private static void Part2(SensorNetwork sensorNetwork)
	{
		int searchMinX = 0;
		int searchMaxX = 4000000;
		int searchMinY = 0;
		int searchMaxY = 4000000;

		for (int y = searchMinY; y <= searchMaxY; y++)
		{
			IEnumerable<(int a, int b)> coverageRanges = sensorNetwork.GetConverage(y, searchMinX, searchMaxX);
			if (coverageRanges.Count() > 1)
			{
				long tuningFrequency = (coverageRanges.Select(r => r.b).Min() + 1) * 4000000L + y;
				Console.WriteLine($"tuning frequency: {tuningFrequency}");
				return;
			}
		}
	}
}



