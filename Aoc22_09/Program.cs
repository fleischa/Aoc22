namespace Aoc22_09;

using AocCommon;

internal class Program
{
    private static async Task Main(string[] args)
    {
        RopeSpace ropeSpace = new(10);

        await new SingleLineProcessor<RopeSpace>((line, rs) =>
        {
            string[] tokens = line.Split(' ');

            (int x, int y) move;

            switch (tokens[0])
            {
                case "L":
                    move = new(-1, 0);
                    break;
                case "U":
                    move = new(0, 1);
                    break;
                case "R":
                    move = new(1, 0);
                    break;
                case "D":
                    move = new(0, -1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            for (int i = 0; i < int.Parse(tokens[1]); i++)
            {
                rs!.MoveHead(move);
            }
        }).Process(ropeSpace);

        Console.WriteLine($"unique tail locations: {ropeSpace.UniqueTailLocations.Count}");
    }
}

