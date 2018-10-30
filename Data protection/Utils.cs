using System;
using System.IO;
using System.Threading;

namespace Data_protection
{
	internal static class Utils
	{
		public static int Gcd(int m, int n)
		{
			while (n!=0)
			{
				var t = n;
				n = m % n;
				m = t;
			}

			return Math.Abs(m);
		}

		public static int[] GetNumbers(string keyInputText)
		{
			var numbers = keyInputText.Split(' ');

			var numbersInts = new int[numbers.Length];

			for (var i = 0; i < numbers.Length; i++)
			{
				if (!int.TryParse(numbers[i], out numbersInts[i]))
				{
					throw new InvalidDataException("Entered key is not an integer");
				}
			}
			
			for (var i = 0; i < 2; i++)
			{
				if (numbersInts[i]<100||numbersInts[i]>999)
				{
					throw new InvalidDataException("Entered numbers are incorrect. (Must be from [100; 999])");
				}
			}

			if (Gcd(numbersInts[0], numbersInts[1]) != 1)
			{
				throw new InvalidDataException("Numbers are not coprime");
			}

			if (numbersInts[0] % 4 != 3 || numbersInts[1] % 4 != 3)
			{
				throw new InvalidDataException("Numbers are not congruent to 3 (mod 4)");
			}
			return numbersInts;
		}

		public static int GetCoprime(int i)
		{
			var random = new Random();
			while (true)
			{
				var m = random.Next(i);
				if (m>1 && Gcd(m, i) == 1)
				{
					return m;
				}
			}
		}
	}
}