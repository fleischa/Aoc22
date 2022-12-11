namespace Aoc22_11;

public class Monkey
{
    public Monkey(IEnumerable<int> startingItems, MonkeyOperation operation, int parameter, int testModulo, int targetMonkeyOnTestSuccess, int targetMonkeyOnTestFailure)
    {
        this.Items = new Queue<int>(startingItems);
        this.Operation = operation;
        this.Parameter = parameter;
        this.TestModulo = testModulo;
        this.TargetMonkeyOnTestSuccess = targetMonkeyOnTestSuccess;
        this.TargetMonkeyOnTestFailure = targetMonkeyOnTestFailure;
    }

    public Queue<int> Items { get; }

    public MonkeyOperation Operation { get; }

    public int Parameter { get; }

    public int TestModulo { get; }

    public int TargetMonkeyOnTestSuccess { get; }

    public int TargetMonkeyOnTestFailure { get; }

    public int Activity { get; set; }
}
