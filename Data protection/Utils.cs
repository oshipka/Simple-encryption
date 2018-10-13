using System;
using System.Collections.Generic;
using System.Linq;

namespace Data_protection
{
	internal static class Utils
	{
		private const string EngAlphabet = "abcdefghijklmnopqrstuvwxyz.,\"()!?:-";
		private const string CyrAlphabet = "абвгдеєжзиіїйклмнопрстуфхцчшщюяь.,\"()!?:-'";



		internal struct Pair : IComparable<Pair>
		{
			public char Letter;
			public int Counter;

			public int CompareTo(Pair obj)
			{
				return Counter.CompareTo(obj.Counter);
			}

			public static void Sort(List<Pair> list)
			{
				list.Sort((a, b) => b.Counter.CompareTo(a.Counter));
			}
		}
	}
}
