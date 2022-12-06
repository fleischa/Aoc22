namespace Aoc22_06;

internal class Program
{
	private static async Task Main(string[] args)
	{
		using StreamReader reader = File.OpenText("input.txt");
		string signal = await reader.ReadToEndAsync();

		Console.WriteLine($"start-of-packet: {Program.FindMarker(signal, 4)}");
		Console.WriteLine($"start-of-message: {Program.FindMarker(signal, 14)}");
	}

	private static int FindMarker(string signal, int markerLength)
	{
		return Enumerable.Range(markerLength, signal.Length)
			.First(i => signal.Substring(i - markerLength, markerLength).Distinct().Count() == markerLength);
	}
}