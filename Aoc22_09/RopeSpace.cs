namespace Aoc22_09;

internal class RopeSpace
{
    public RopeSpace(int ropeLength)
    {
        this.Rope = Enumerable.Repeat<(int x, int y)>(new(0, 0), ropeLength).ToArray();
    }

    public HashSet<(int x, int y)> UniqueTailLocations { get; init; } = new() { new(0, 0) };

    public (int X, int Y)[] Rope { get; init; }

    public void MoveHead((int x, int y) move)
    {
        this.Rope[0] = new(this.Rope[0].X + move.x, this.Rope[0].Y + move.y);

        for (int i = 0; i < this.Rope.Length - 1; i++)
        {
            (int X, int Y) h = this.Rope[i];
            (int X, int Y) t = this.Rope[i + 1];

            int dX = h.X - t.X;
            int dY = h.Y - t.Y;

            if (Math.Abs(dX) < 2 && Math.Abs(dY) < 2)
            {
                // h and t are adjacent
                continue;
            }

            this.Rope[i + 1] = new(t.X + Math.Sign(dX), t.Y + Math.Sign(dY));
        }

        this.UniqueTailLocations.Add(this.Rope.Last());
    }
}
