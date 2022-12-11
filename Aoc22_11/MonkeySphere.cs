namespace Aoc22_11;

using System.Numerics;

public class MonkeySphere
{
    public MonkeySphere(params Monkey[] monkeys)
    {
        this.Monkeys = monkeys;
    }

    public Monkey[] Monkeys { get; }

    public int MonkeyBusiness
    {
        get
        {
            Monkey[] mostActive = this.Monkeys.OrderByDescending(m => m.Activity).Take(2).ToArray();
            return mostActive[0].Activity * mostActive[1].Activity;
        }
    }

    private void ProcessMonkey(Monkey monkey, bool reduceWorry)
    {
        while (monkey.Items.TryDequeue(out int item))
        {
            switch (monkey.Operation)
            {
                case MonkeyOperation.Add:
                    item += monkey.Parameter;
                    break;
                case MonkeyOperation.Multiply:
                    item *= monkey.Parameter;
                    break;
                case MonkeyOperation.Square:
                    item *= item;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            if (reduceWorry)
            {
                item /= 3;
            }

            monkey.Activity++;

            int targetMonkey = item % monkey.TestModulo == 0 ? monkey.TargetMonkeyOnTestSuccess : monkey.TargetMonkeyOnTestFailure;
            this.Monkeys[targetMonkey].Items.Enqueue(item);
        }
    }

    public void ProcessRounds(int numberOfRounds, bool reduceWorry)
    {
        for (int i = 0; i < numberOfRounds; i++)
        {
            foreach (Monkey monkey in this.Monkeys)
            {
                this.ProcessMonkey(monkey, reduceWorry);
            }

            Console.WriteLine($"processed {i} rounds");
        }
    }
}





