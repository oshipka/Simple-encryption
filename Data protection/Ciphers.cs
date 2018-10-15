using System;
using System.IO;
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
					string toRemember = null;
					for (var i = position; i < position + blockLength; i++)
					{
						
						if(i<inputText.Length)
						{
							block += inputText[i];
						}
						else
						{
							block += " ";
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

			public static string GammaMethod()
			{
				throw new System.NotImplementedException();
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

			public static string GammaMethod()
			{
				throw new System.NotImplementedException();
			}
		}
		
		private static class Utils
		{
			public static int[][] HKeyToMatrix(string key, string alphabet)
			{
				var k = 0;
				var result = new int[(int)Sqrt(key.Length)][];
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
		}
	}
}
