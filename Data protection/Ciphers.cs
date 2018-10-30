using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Math;

namespace Data_protection
{
	public class Ciphers
	{


		public static class Cipher
		{
			public static string BBSMethod(string inputText, int[] key, out int seed)
			{
				var result = "";
				var bbsSequence = Utils.GenerateBBSSequence(key, inputText.Length, out seed);

				foreach (var character in inputText)
				{
					 result += (char)((int) character ^ bbsSequence.Dequeue());
				}
				
				return result;
			}
		}

		public static class Decipher
		{
			public static string BBSMethod(string inputText, int[] key)
			{
				var result = "";
				var bbsSequence = Utils.GenerateBBSSequence(key, inputText.Length, out var seed);

				foreach (var character in inputText)
				{
					result += (char)((int) character ^ bbsSequence.Dequeue());
				}
				
				return result;

			}

			
		}

		private static class Utils
		{
			public static Queue<int> GenerateBBSSequence(int[] key, int textLength, out int seed)
			{
				var result = new Queue<int>();
				var n = key[0]*key[1];
				int x;
				seed = 0;
				if (key.Length == 2)
				{
					x = Data_protection.Utils.GetCoprime(n);
					seed = x;	
				}
				else
				{
					x = key[2];
				}
				
				
				for (int i = 0; i < textLength; i++)
				{
					x = (int) Pow(x, 2) % n;
					result.Enqueue(x);
				}

				return result;

			}
		}
	}
}
