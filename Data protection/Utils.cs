using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_protection
{
    class Utils
    {
        public static string alphabet = "abcdefghijklmnopqrstuvwxyz";
        public static string frequencyAlphabet = "etaoinshrdlcumwfgypbvkjxqz";


        public static string Cipher(string inputText, bool? caesar, bool? vigener, int key, string keyword)
        {
            inputText = inputText.ToLower();
            keyword = keyword.ToLower();
            if (caesar == true)
            {
                return Caesar(inputText, key, keyword);
            }
            if (vigener == true)
            {
                return Vigener(inputText, keyword);
            }

            return "0";
        }

        public static string Decipher(string inputText, bool? caesar, bool? vigener, bool? keyKnown, int key, string keyword)
        {
            inputText = inputText.ToLower();
            keyword = keyword.ToLower();
            if (caesar == true)
            {
                return DCaesar(inputText, keyKnown, key, keyword);
            }
            if (vigener == true)
            {
                return DVigener(inputText, keyKnown, keyword);
            }
            return "1";
        }

        private static string DVigener(string inputText, bool? keyKnown, string keyword)
        {
            if (keyKnown == true)
            {
                string keywordAlphabet = CaesarEditAlphabet(keyword);
                string result = "";
                int j = 0;
                foreach (char x in inputText)
                {
                    if (x == ' ' || !alphabet.Contains(x))
                    {
                        result += x;
                    }
                    else
                    {
                        int t1 = alphabet.IndexOf(x) - alphabet.IndexOf(keyword[j]);
                        int t2 = t1 % 26;
                        if (t2 < 0)
                        {
                            t2 = 26 + t2;
                        }
                        result += alphabet[t2];
                        j = (j + 1) % keyword.Length;
                    }
                }

                return result;
            }
            if (keyKnown == false)
            {
                return "Mate, I won't bruteforce this text, sorry.";
            }
            else
            {
                return "How'd you even managed to get this exception? That checkbox returned null, man.";
            }
        }
        private static string DCaesar(string inputText, bool? keyKnown, int key, string keyword)
        {
            if(keyKnown == true)
            {
                string keywordAlphabet = CaesarEditAlphabet(keyword);
                string result = "";
                key = key % 26;


                foreach (char x in inputText)
                {
                    if (x == ' ' || !alphabet.Contains(x))
                    {
                        result += x;
                    }
                    else
                    {
                        int t1 = keywordAlphabet.IndexOf(x) - key;
                        int t2 = t1 % 26;
                        if (t2 < 0)
                        {
                            t2 = 26 + t2;
                        }
                        result += alphabet[t2];
                    }
                }

                return result;
            }
            if(keyKnown == false)
            {
                return frequencyAnalysis(inputText);
            }
            else
            {
                return "How'd you even managed to get this exception? That checkbox returned null, man.";
            }
        }

        private static string frequencyAnalysis(string inputText)
        {
            

            int maxSum = 0;
            string result = "";

            
                getPermutations(alphabet.ToCharArray(), ref maxSum, ref result, inputText);

            

            return result;
        }
        private static void Swap(ref char a, ref char b)
        {
            if (a == b) return;

            a ^= b;
            b ^= a;
            a ^= b;
        }
        public static void getPermutations(char[] list, ref int maxSum, ref string result, string inputText)
        {
            int x = list.Length - 1;
            GetPermutations(list, 0, x, ref maxSum, ref result, inputText);
        }
        private static void GetPermutations(char[] list, int recursionDepth, int maxDepth, ref int maxSum, ref string result, string inputText)
        {
            if (recursionDepth == maxDepth)
            {
                for (int key = 0; key < alphabet.Length; key++)
                {

                    int currentSum = 0;
                    string currentResult = DCaesar(inputText, true, key, list.ToString());

                    var thisFrequencyAlphabet = new List<pair>();
                    foreach (char letter in alphabet)
                    {
                        var pair = new pair();
                        pair.letter = letter;
                        pair.counter = currentResult.Count(f => f == letter);
                        thisFrequencyAlphabet.Add(pair);
                    }
                    pair.Sort(thisFrequencyAlphabet);
                    thisFrequencyAlphabet.ToArray();
                    string thisfrequencyAlphabet = "";
                    foreach (pair x in thisFrequencyAlphabet)
                    {
                        thisfrequencyAlphabet = string.Concat(x.letter, thisfrequencyAlphabet);
                    }

                    for (int i = 0; i < thisfrequencyAlphabet.Length; i++)
                    {
                        currentSum += i * alphabet.IndexOf(thisfrequencyAlphabet[i]);
                    }

                    if (currentSum > maxSum)
                    {
                        result = currentResult;
                        maxSum = currentSum;
                    }
                    System.Diagnostics.Trace.WriteLine(currentResult);
                }
            }
            else
                for (int i = recursionDepth; i <= maxDepth; i++)
                {
                    Swap(ref list[recursionDepth], ref list[i]);
                    GetPermutations(list, recursionDepth + 1, maxDepth, ref maxSum, ref result, inputText);
                    Swap(ref list[recursionDepth], ref list[i]);
                    
                }
        }

        public static string Caesar(string inputText, int key, string keyword)
        {
            string keywordAlphabet = CaesarEditAlphabet(keyword);
            string result = "";
            key = key % 26;


            foreach (char x in inputText)
            {
                if (x == ' '|| !alphabet.Contains(x))
                {
                    result += x;
                }
                else
                {
                    int t1 = alphabet.IndexOf(x) + key;
                    int t2 = t1 % 26;
                    if (t2 < 0)
                    {
                        t2 = 26 + t2;
                    }
                    result += keywordAlphabet[t2];
                }
            }

            return result;
        }
        public static string Vigener(string inputText, string keyword)
        {
            string result = "";

            int j = 0;
            foreach (char x in inputText)
            {
                if (x == ' ')
                {
                    result += " ";
                }
                else
                {
                    int t1 = alphabet.IndexOf(x)+ alphabet.IndexOf(keyword[j]);
                    int t2 = t1 % 26;
                    if (t2 < 0)
                    {
                        t2 = 26 + t2;
                    }
                    result += alphabet[t2];
                    j = (j + 1) % keyword.Length;
                }
            }
            return result;
        }

        static string CaesarEditAlphabet(string keyword)
        {
            var word = new List<char>();
            for (int i = 0; i < keyword.Length; i++)
            {
                if(!word.Contains(keyword[i]))
                {
                word.Add(keyword[i]);
                }
            }

            var lAlphabet = new List<char>();
            for (int i = 0; i < alphabet.Length; i++)
            {
                lAlphabet.Add(alphabet[i]);
            }
            var selection = (from x in lAlphabet
                             where !word.Contains(x)
                             select x).ToArray();
            string result = "";
            foreach (char x in word)
            {
                result = string.Concat(result, x.ToString());
            }
            foreach (char x in selection)
            {
                 result = string.Concat(result, x.ToString());
            }
            
            return result;
        }
    }

    struct pair: IComparable<pair>
    {
        public char letter;
        public int counter;

        public int CompareTo(pair obj)
        {
            return counter.CompareTo(obj.counter);
        }

        public static void Sort(List<pair> list)
        {
            list.Sort((a, b) => a.counter.CompareTo(b.counter));
        }
    }
}
