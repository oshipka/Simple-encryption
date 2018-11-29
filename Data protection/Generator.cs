using System;
using System.Numerics;
using System.Windows.Documents;

namespace Data_protection
{
	public class Generator
	{
		private static int _p;
		private static int _a;
		private static int _randA;
		private static int _randB;
		private static int _X;
		private static int _Y;
		private static int _k;
		private static int _kp;

		public Generator()
		{
			_p = /*22697;*/ GetRandomPrimeNumber();
			_a = GetPrimaryRoot(_p);
			_randA = GetRandomNumber();
			_randB = GetRandomNumber();
			_X = CalculateA(_randA);
			_Y = CalculateA(_randB);
			_k = CalculateK(_Y, _randA);
			_kp = CalculateK(_X, _randB);
		}

		private static int GetRandomPrimeNumber()
		{
			var random = new Random();
			var randomVal = random.Next(10, 100);
			var result = 6 * randomVal + (int) Math.Pow(-1, randomVal);
			return result;
		}

		private static int GetRandomNumber()
		{
			var random = new Random();
			return random.Next(10, 100);
		}

		private static int GetPrimaryRoot(int val)
		{
			for (var a = 1; a < val + 1; a++)
			{
				var cnt = 0;
				for (var k = 1; k < val; k++)
				{
					Console.WriteLine(k);
					if (new BigInteger(Math.Pow(a, k)) % val == 1)
					{
						cnt += 1;
					}
				}

				if (cnt == 1 && new BigInteger(Math.Pow(a, val - 1)) % val == 1)
				{
					return a;
				}
			}

			throw new Exception("Could not calculate primary root");
		}

		private static int CalculateA(int power)
		{
			return (int) Math.Pow(_a, power) % _p;
		}

		private static int CalculateK(int val, int power)
		{
			return (int) Math.Pow(val, power) % _p;
		}

		private static int Phi(int n)
		{
			var ret = 1;
			for (var i = 2; i * i <= n; ++i)
			{
				var p = 1;
				while (n % i == 0)
				{
					p *= i;
					n /= i;
				}

				if ((p /= i) >= 1) ret *= p * (i - 1);
			}

			return --n == 1 ? n * ret : ret;
		}

		public int GetP()
		{
			return _p;
		}

		public int GetA()
		{
			return _a;
		}

		public int GetRandA()
		{
			return _randA;
		}

		public int GetRandB()
		{
			return _randB;
		}

		public int GetX()
		{
			return _X;
		}

		public int GetY()
		{
			return _Y;
		}

		public int GetK()
		{
			return _k;
		}

		public int GetKP()
		{
			return _kp;
		}
	}
}