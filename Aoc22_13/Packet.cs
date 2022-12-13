namespace Aoc22_13;

using System.Text;

public class Packet : IComparable<Packet>
{
	public int Value { get; private set; }

	public List<Packet>? SubPackets { get; private set; }

	public int CompareTo(Packet? other)
	{
		if (other == null)
		{
			throw new ArgumentNullException(nameof(other));
		}

		if (this.SubPackets == null && other.SubPackets == null)
		{
			return this.Value - other.Value;
		}

		List<Packet> compThis = this.GetHybridComparisonList();
		List<Packet> compOther = other.GetHybridComparisonList();

		for (int i = 0; i < Math.Max(compThis.Count, compOther.Count); i++)
		{
			if (i + 1 > compThis.Count)
			{
				return -1;
			}

			if (i + 1 > compOther.Count)
			{
				return 1;
			}

			int c = compThis[i].CompareTo(compOther[i]);

			if (c != 0)
			{
				return c;
			}
		}

		return 0;
	}

	public static Packet Parse(string s)
	{
		Stack<Packet> listPacketStack = new();
		StringBuilder builder = new();
		Packet? result = null;
		
		foreach (char c in s)
		{
			switch (c)
			{
				case '[':
					Packet listPacket = new() { SubPackets = new List<Packet>() };
					if (listPacketStack.Any())
					{
						listPacketStack.Peek().SubPackets!.Add(listPacket);
					}

					listPacketStack.Push(listPacket);
					break;
				case ']':
					if (builder.Length > 0)
					{
						listPacketStack.Peek().SubPackets!.Add(new Packet { Value = int.Parse(builder.ToString()) });
					}
					builder.Clear();
					result = listPacketStack.Pop();
					break;
				case ',':
					if (builder.Length > 0)
					{
						listPacketStack.Peek().SubPackets!.Add(new Packet { Value = int.Parse(builder.ToString()) });
					}
					builder.Clear();
					break;
				default:
					builder.Append(c);
					break;
			}
		}

		return result!;
	}

	public static bool operator <(Packet left, Packet right)
	{
		return left.CompareTo(right) < 0;
	}

	public static bool operator >(Packet left, Packet right)
	{
		return left.CompareTo(right) > 0;
	}

	public static bool operator <=(Packet left, Packet right)
	{
		return left.CompareTo(right) < 1;
	}

	public static bool operator >=(Packet left, Packet right)
	{
		return left.CompareTo(right) > -1;
	}

	public override string ToString()
	{
		if (this.SubPackets != null)
		{
			return $"[{string.Join(',', this.SubPackets.Select(s => s.ToString()))}]";
		}

		return this.Value.ToString();
	}

	private List<Packet> GetHybridComparisonList()
	{
		if (this.SubPackets != null)
		{
			return this.SubPackets;
		}

		return new List<Packet> { new() { Value = this.Value } };
	}
}


















