using System;
using System.Numerics;
using System.Windows.Documents;

namespace Data_protection
{
	public class Generator
	{
		private static BigInteger _p;
		private static BigInteger _a;
		private static BigInteger _randA;
		private static BigInteger _randB;
		private static BigInteger _X;
		private static BigInteger _Y;
		private static BigInteger _k;
		private static BigInteger _kp;

		public Generator()
		{
			var random = new Random();
			
			_p = /*22697;*/ GetRandomPrimeNumber(random);
			_a = GetPrimaryRoot(_p);
			_randA = GetRandomNumber(random);
			_randB = GetRandomNumber(random);
			_X = CalculateA(_randA);
			_Y = CalculateA(_randB);
			_k = CalculateK(_Y, _randA);
			_kp = CalculateK(_X, _randB);
		}

		private static BigInteger GetRandomPrimeNumber(Random random)
		{
			var randomVal = random.Next(100, 1000);
			var result = 6 * randomVal + (BigInteger) Math.Pow(-1, randomVal);
			if (!CheckStatic(result))
			{
				result = GetRandomPrimeNumber(random);
			}

			return result;
		}

		private static BigInteger GetRandomNumber(Random random)
		{
			return random.Next(100, 1000);
		}

		private static BigInteger GetPrimaryRoot(BigInteger val)
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

		private static BigInteger CalculateA(BigInteger power)
		{
			return (BigInteger) Pow(_a, power) % _p;
		}

		private static BigInteger CalculateK(BigInteger val, BigInteger power)
		{
			return (BigInteger) Pow(val, power) % _p;
		}

		private static BigInteger Pow(BigInteger a, BigInteger pov)
		{
			BigInteger result = 1;
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

		private static bool CheckStatic(BigInteger n)
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
		
		private static BigInteger Phi(BigInteger n)
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

		public BigInteger GetP()
		{
			return _p;
		}

		public BigInteger GetA()
		{
			return _a;
		}

		public BigInteger GetRandA()
		{
			return _randA;
		}

		public BigInteger GetRandB()
		{
			return _randB;
		}

		public BigInteger GetX()
		{
			return _X;
		}

		public BigInteger GetY()
		{
			return _Y;
		}

		public BigInteger GetK()
		{
			return _k;
		}

		public BigInteger GetKP()
		{
			return _kp;
		}
	}
}