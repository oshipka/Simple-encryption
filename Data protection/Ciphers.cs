using System;
using System.Collections.Generic;
using System.Text;
using static System.Math;

namespace Data_protection
{
	public class Ciphers
	{
		Random random = new Random();
		
		public class PrivateKey
		{
			public static int _p;
			public static int _q;
			public static int _x;
			public static int _numBits;

			public int p()
			{
				return _p;
			}

			public int q()
			{
				return _q;
			}

			public int x()
			{
				return _x;
			}

			public int numBits()
			{
				return _numBits;
			}

			private PrivateKey(int p, int q, int x, int numBits = 0)
			{
				_p = p;
				_q = q;
				_x = x;
				_numBits = numBits;
			}

			public static Dictionary<string, int> to_dict()
			{
				var res = new Dictionary<string, int>();
				res.Add("p", _p);
				res.Add("q", _q);
				res.Add("x", _x);
				res.Add("numBits", _numBits);
				return res;
			}

			public static PrivateKey from_dict(Dictionary<string, int> data)
			{
				foreach (var key in data)
				{
					if (!"p q x numBits".Contains(key.Key))
					{
						throw new Exception("Invalid data key");
					}
				}

				return new PrivateKey(data["p"], data["q"], data["x"], data["numBits"]);
			}
		}

		public class PublicKey
		{
			public static int _p;
			public static int _q;
			public static int _y;
			public static int _numBits;

			public int p()
			{
				return _p;
			}

			public int q()
			{
				return _q;
			}

			public int y()
			{
				return _y;
			}

			public int numBits()
			{
				return _numBits;
			}

			private PublicKey(int p, int q, int y, int numBits = 0)
			{
				_p = p;
				_q = q;
				_y = y;
				_numBits = numBits;
			}

			public static Dictionary<string, int> to_dict()
			{
				var res = new Dictionary<string, int>();
				res.Add("p", _p);
				res.Add("q", _q);
				res.Add("y", _y);
				res.Add("numBits", _numBits);
				return res;
			}

			public static PublicKey from_dict(Dictionary<string, int> data)
			{
				foreach (var key in data)
				{
					if (!"p q y numBits".Contains(key.Key))
					{
						throw new Exception("Invalid data key");
					}
				}

				return new PublicKey(data["p"], data["q"], data["y"], data["numBits"]);
			}
		}

		private class ElGamal
		{
			private PublicKey _publicKey;
			private PrivateKey _privateKey;

			public string Encrypt(PublicKey pub_key, string plain_text)
			{
				var z = Encode(plain_text, pub_key.numBits());
				cipher_pairs =  []
				for encoded_char in
				k = random.randint(1, pub_key.p)
				a = pow(pub_key.q, k, pub_key.p)
				b = (encoded_char * pow(pub_key.y, k, pub_key.p)) % pub_key.p
				cipher_pairs.append([a, b])

				encrypted_str = ''
				for pair in
				encrypted_str += str(pair[0]) + ' ' + str(pair[1]) + ' '
				return encrypted_str

				def
				decrypt(self, priv_key, cipher_text) :
				plaintext =  []
				cipher_array = cipher_text.split()
				if not
				len(cipher_array) % 2 == 0:
				return "Malformed Cipher Text"
				for i in
				range(0, len(cipher_array), 2):
				a = int(cipher_array[i])
				b = int(cipher_array[i + 1])

				s = pow(a, priv_key.x, priv_key.p)
				plain = (b * pow(s, priv_key.p - 2, priv_key.p)) % priv_key.p
				plaintext.append(plain)

				decrypted_text = self.__decode(plaintext, priv_key.num_bits)
				return ''.join([ch for ch in
				decrypted_text if ch != '\x00'])
			}

			public PrivateKey generate_keys(out PrivateKey pk, out PublicKey pubK, int num_bits = 256, int size = 32)
			{
				var p = Utils.GetRandomPrimeNumber(num_bits, size)
				var q = self.__find_prime(num_bits, size)

				x = random.randint(1, Floor((p - 1) / 2));
				y = pow(q, x, p)
				return PrivateKey(p, q, x, num_bits), PublicKey(p, q, y, num_bits)
			}

			private static bool IsPrime(int num, int test_count)
			{
				if (num == 1)
				{
					return False
				}

				if (test_count >= num)
				{
					test_count = num - 1
				for x in
				range(test_count) :
				val = random.randint(1, num - 1)
				if pow(val, num - 1, num) != 1:
				return False
				return True
			}

			private int FindPrime(int num_bits, int size)
			{
				int p;
				
				while
				p = random.Next(2 * *(num_bits - 2), 2 * *(num_bits - 1))
				while p % 2 == 0:
				p = random.Next(2 * *(num_bits - 2), 2 * *(num_bits - 1))
				while not self.__is_prime(p, size):
				p = random.Next(2 * *(num_bits - 2), 2 * *(num_bits - 1))
				while p % 2 == 0:
				p = random.Next(2 * *(num_bits - 2), 2 * *(num_bits - 1))
				p = p * 2 + 1
				if self.__is_prime(p, size):
				return p
			}

			public static string Encode(string plain_text, int num_bits)
			{
				byte[] byte_array = Encoding.UTF8.GetBytes(plain_text);
				var z = [];
				var k = Floor(num_bits / 8.0);
				var j = -1 * k;
				for (int i = 0; i < byte_array.Length; i++)
				{
					
				}
				if i % k == 0:
				j += k
				z.append(0)

				z[Floor(j / k)] += byte_array[i] * (2 ** (8 * (i % k)))
				return z;
			}

			public static string Decode(string cipherText, int numBits, string encoding = "utf-8")
			{
				var bytesArray = new List<byte[]>();
				var k = Floor(numBits / 8.0);
				foreach (var symbol in cipherText)
				{
					for (int i = 0; i < k; i++)
					{
						var temp = int.Parse(symbol.ToString());
						for (int j = i+1; j < k; j++)
						{
							temp = temp % (int) Pow(2, (8 * j));
						}

						var letter = temp;
						var bts = BitConverter.GetBytes(letter);
						Array.Reverse(bts);
						bytesArray.Add(bts);
					}
				}

				var decodedText = "";
				foreach (var i in bytesArray)
				{
					decodedText += Encoding.UTF8.GetString(i);
				}

				return decodedText;
		}
	}
}