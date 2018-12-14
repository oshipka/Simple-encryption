using System;

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

		public static int GetRandomPrimeNumber(Random random,  int max)
		{
			var randomVal = random.Next(1, max);
			var result = 6 * randomVal + (int) Math.Pow(-1, randomVal);
			if (!CheckStatic(result))
			{
				result = GetRandomPrimeNumber(random, max);
			}

			return result;
		}

		public static int GetRandomNumber(Random random)
		{
			return random.Next(100, 1000);
		}

		public static int GetPrimaryRoot(int val)
		{
			for (var a = 1; a < val + 1; a++)
			{
				var cnt = 0;
				for (var k = 1; k < val; k++)
				{
					Console.WriteLine(k);
					if (Pow(a, k) % val == 1)
					{
						cnt += 1;
					}
				}

				if (cnt == 1 && Pow(a, val - 1) % val == 1)
				{
					return a;
				}
			}

			throw new Exception("Could not calculate primary root");
		}
		
		private static int Pow(int a, int pov)
		{
			int result = 1;
			var oddity = pov % 2;
			var div = pov / 2;

			while (div != 0)
			{

				result *= a*a;
				div--;
			}
		
			if (oddity == 1)
			{
				result *= a;
			}
			return result;
		}
		
		private static bool CheckStatic(int n)
		{
			if (n == 2 || n == 3)
				return true;
			if (n < 2 || n % 2 == 0)
				return false;
			if (n < 9)
				return true;
			if (n % 3 == 0)
				return false;
			var r = (n /2);
			var f = 5;
			while (f <= r)
			{
				if (n % f == 0)
					return false;
				if (n % (f + 2) == 0)
					return false;
				f += 6;
			}

			return true;
		}

		public static int GPowXModP(int g, int x, int p)
		{
			return Pow(g, x) % p;
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