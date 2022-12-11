namespace Aoc22_11;

internal class Program
{
    private static void Main(string[] args)
    {
        //MonkeyPack monkeyPack = Program.CreateMonkeyPack();
        //monkeyPack.ProcessRounds(20);
        //Console.WriteLine($"monkey business: {monkeyPack.MonkeyBusiness}");

        MonkeyPack monkeyPack = Program.CreateMonkeyPack();
        monkeyPack.ProcessRounds(10000);
        Console.WriteLine($"serious monkey business: {monkeyPack.MonkeyBusiness}");
    }

    private static MonkeyPack CreateMonkeyPack()
    {
        return new(
            new Monkey(new[] { 85, 77, 77 }, MonkeyOperation.Multiply, 7, 19, 6, 7),
            new Monkey(new[] { 80, 99 }, MonkeyOperation.Multiply, 11, 3, 3, 5),
            new Monkey(new[] { 74, 60, 74, 63, 86, 92, 80 }, MonkeyOperation.Add, 8, 13, 0, 6),
            new Monkey(new[] { 71, 58, 93, 65, 80, 68, 54, 71 }, MonkeyOperation.Add, 7, 7, 2, 4),
            new Monkey(new[] { 97, 56, 79, 65, 58 }, MonkeyOperation.Add, 5, 5, 2, 0),
            new Monkey(new[] { 77 }, MonkeyOperation.Add, 4, 11, 4, 3),
            new Monkey(new[] { 99, 90, 84, 50 }, MonkeyOperation.Square, 0, 17, 7, 1),
            new Monkey(new[] { 50, 66, 61, 92, 64, 78 }, MonkeyOperation.Add, 3, 2, 5, 1));
    }
}




