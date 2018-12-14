using System;
using static System.Math;
using static Data_protection.Ciphers;

namespace Data_protection
{
	public class Signature
	{
		Random random = new Random();
		
		public int hash_func(string @string)
		{
			var hashSum = 0;
			foreach (var @char in @string)
			{
				hashSum += ((@char+ 256) % 128);
			}

			return hashSum;
		}

		public int mod_inv(int num, int mod)
		{
			for (int i = 0; i <= mod + 1; i++)
			{
				if ((num * i) % mod == 1)
					return i;
			}

			throw new Exception("ERROR: modulo {mod} inverse of {num} does not exists!");
		}

		public int gcd(int a, int b)
		{
			while (b != 0)
			{
				var temp = a;
				a = b;
				b = temp % b;
			}

			return a;
		}

		public bool is_co_prime(int firstNum, int secondNum)
		{
			return 1 == gcd(firstNum, secondNum);
		}

		public int[] sign(PrivateKey privKey, int m)
		{
			m = hash_func(m.ToString());
			var k = random.Next(1, privKey.p() - 2);
			int r=0, s=0;
			while (!is_co_prime(k, privKey.p() - 1))
			{
				k = random.Next(1, privKey.p() - 2);
				r = (int)Pow(privKey.q(), k) % privKey.p();
				s = mod_inv(k, privKey.p() - 1) * (m - privKey.x() * r) % (privKey.p() - 1);
			}

			return new[] {r, s};
		}

		public bool verify(PublicKey pub_key, int[] sig, int m)
		{

			var r = (sig[0]);
			var s = (sig[1]);
			m = hash_func(m.ToString());
			if (r < 1 || r > pub_key.p() - 1)
			{
				return false;
			}

			var v1 = (((int) Pow(pub_key.y(), r) % pub_key.p()) * ((int)Pow(r, s) % pub_key.p())) % pub_key.p();
			var v2 = (int)Pow(pub_key.q(), m) % pub_key.p();
// y^r * r^s = g^m % p
			return v1 == v2;
		}
	}
}