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
			public static string HillMethod(string inputText, string key, string alphabet)
			{
				var blockLength = (int) Sqrt(key.Length);
				var keyMatrix = Utils.HKeyToMatrix(key, alphabet);
				if (Data_protection.Utils.Determinant(keyMatrix).Equals(0)||Data_protection.Utils.Gcd((int)Data_protection.Utils.Determinant(keyMatrix), alphabet.Length)!=1)
				{
					throw new ArgumentException("Can not cipher with current key");
				}

				inputText = inputText.Replace(' ', '_');
				var result = "";
				var position = 0;
				while (position + blockLength < inputText.Length)
				{
					var block = "";
					for (var i = position; i < position + blockLength; i++)
					{
						
						if(i<inputText.Length)
						{
							block += inputText[i];
						}
						else
						{
							block += "_";
						}
					}

					var blockVector = Utils.HBlockToVector(block, alphabet);
					var resultVector =
						Data_protection.Utils.MultiplyMatrices(keyMatrix, blockVector, alphabet.Length);
					for (var i = 0; i < blockLength; i++)
					{
						result += alphabet[resultVector[i][0]];
					}

					position += blockLength;
				}

				result = result.Replace('_', ' ');
			return result;
			}

			public static string GammaMethod(string inputText, string key, string alphabet)
			{
				inputText = inputText.ToLower();
				inputText = inputText.Replace(' ', alphabet[alphabet.Length - 1]);
				var inputTextList = Utils.GToList(inputText, alphabet);
				var keySplit = key.Split(' ');
				var keyList = new List<int>();
				foreach (var keyString in keySplit)
				{
					if (!int.TryParse(keyString, out var number))
					{
						throw new InvalidDataException("Entered key was not an integer.");
					}

					keyList.Add(number);
				}

				Utils.GGenerateGamma(ref keyList, inputTextList.Count, alphabet.Length);
				var result = "";
				for (var i = 0; i < inputTextList.Count; i++)
				{
					result += alphabet[(inputTextList[i] + keyList[i]) % alphabet.Length];
				}

				result = result.Replace(alphabet[alphabet.Length - 1], ' ');
				return result;
			}
		}

		public static class Decipher
		{
			public static string HillMethod(string inputText, string key, string alphabet)
			{
				var blockLength = (int) Sqrt(key.Length);
				var reverseKeyMatrix = Data_protection.Utils.ReverseMatrix(Utils.HKeyToMatrix(key, alphabet), alphabet.Length);
				

				inputText = inputText.Replace(' ', '_');
				var result = "";
				var position = 0;
				while (position + blockLength < inputText.Length)
				{
					var block = "";
					for (var i = position; i < position + blockLength; i++)
					{

						if (i < inputText.Length)
						{
							block += inputText[i];
						}
						else
						{
							block += "_";
						}
					}

					var blockVector = Utils.HBlockToVector(block, alphabet);
					var resultVector =
						Data_protection.Utils.MultiplyMatrices(reverseKeyMatrix, blockVector, alphabet.Length);
					for (var i = 0; i < blockLength; i++)
					{
						result += alphabet[resultVector[i][0]];
					}

					position += blockLength;
				}

				result = result.Replace('_', ' ');
				return result;
			}

			public static string GammaMethod(string inputText, string key, string alphabet)
			{
				inputText = inputText.Replace(' ', alphabet[alphabet.Length - 1]);
				var inputTextList = Utils.GToList(inputText, alphabet);
				var keySplit = key.Split(' ');
				var keyList = new List<int>();
				foreach (var keyString in keySplit)
				{
					if (!int.TryParse(keyString, out var number))
					{
						throw new InvalidDataException("Entered key was not an integer.");
					}

					keyList.Add(number);
				}

				Utils.GGenerateGamma(ref keyList, inputTextList.Count, alphabet.Length);
				var result = "";
				for (var i = 0; i < inputTextList.Count; i++)
				{
					result += alphabet[(inputTextList[i] + (alphabet.Length - keyList[i])) % alphabet.Length];
				}

				result = result.Replace(alphabet[alphabet.Length - 1], ' ');
				return result;
			}
		}

		private static class Utils
		{
			public static int[][] HKeyToMatrix(string key, string alphabet)
			{
				var k = 0;
				var result = new int[(int) Sqrt(key.Length)][];
				for (var i = 0; i < result.Length; i++)
				{
					result[i] = new int[result.Length];
					for (var j = 0; j < result.Length; j++)
					{
						result[i][j] = alphabet.IndexOf(key[k]);
						k++;
					}
				}

				return result;
			}

			public static int[][] HBlockToVector(string block, string alphabet)
			{
				block = block.ToLower();
				var result = new int[block.Length][];
				for (var i = 0; i < result.Length; i++)
				{

					result[i] = new int[1];
					for (var j = 0; j < 1; j++)
					{
						if (!alphabet.Contains(block[i].ToString()))
						{
							throw new InvalidDataException("Unknown symbol encountered in the text.");
						}

						result[i][j] = alphabet.IndexOf(block[i]);
					}
				}

				return result;
			}

			public static void GGenerateGamma(ref List<int> keys, int requiredLength, int mod)
			{
				if (keys.Count > 3)
				{
					throw new InvalidDataException("Incorrect key entered");
				}

				for (var i = 3; i <= requiredLength; i++)
				{
					keys.Add((keys[i - 1] + keys[i - 3]) % mod);
				}

				for (var i = 0; i < requiredLength; i++)
				{
					keys[i] = (keys[i] + keys[i + 1]) % mod;
				}

				keys.Remove(keys[keys.Count - 1]);
			}

			public static List<int> GToList(string inputText, string alphabet)
			{
				var result = new List<int>();
				foreach (var letter in inputText)
				{
					result.Add(alphabet.IndexOf(letter));
				}

				return result;
			}
		}
	}
}
