using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_protection
{
    internal static class Utils
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        private const string FrequencyAlphabet = "etaoinshrdlcumwfgypbvkjxqz";


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
            if (keyKnown is true)
            {
                var result = "";
                var j = 0;
                foreach (var x in inputText)
                {
                    if (x == ' ' || !Alphabet.Contains(x))
                    {
                        result += x;
                    }
                    else
                    {
                        var t1 = Alphabet.IndexOf(x) - Alphabet.IndexOf(keyword[j]);
                        var t2 = t1 % 26;
                        if (t2 < 0)
                        {
                            t2 = 26 + t2;
                        }

                        result += Alphabet[t2];
                        j = (j + 1) % keyword.Length;
                    }
                }

                return result;
            }
            else if (keyKnown is false)
            {
                return "I won't bruteforce this text, sorry.";
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
                    if (x == ' ' || !Alphabet.Contains(x))
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
                        result += Alphabet[t2];
                    }
                }

                return result;
            }
            if(keyKnown == false)
            {
                return FrequencyAnalysis(inputText);
            }
            else
            {
                return "How'd you even managed to get this exception? That checkbox returned null, man.";
            }
        }

        private static string FrequencyAnalysis(string inputText)
        {
            var result = "";
            var thisFrequencyAlphabet = new List<Pair>();
            foreach (var letter in Alphabet)
            {
                var pair = new Pair();
                pair.Letter = letter;
                pair.Counter = inputText.Count(f => f == letter);
                
                thisFrequencyAlphabet.Add(pair);
            }
            Pair.Sort(thisFrequencyAlphabet);
            var thisFrequencyAlphabetOrdered = "";
            foreach (var letter in thisFrequencyAlphabet)
            {
                thisFrequencyAlphabetOrdered += letter.Letter;
            }
            foreach (var letter in inputText)
            {
                if (Alphabet.Contains(letter))
                {
                    result += FrequencyAlphabet[thisFrequencyAlphabetOrdered.IndexOf(letter)];
                }
                else
                {
                    result += letter;
                }
            }
            

            return result;
        }

        private static string Caesar(string inputText, int key, string keyword)
        {
            var keywordAlphabet = CaesarEditAlphabet(keyword);
            var result = "";
            key = key % 26;


            foreach (var x in inputText)
            {
                if (x == ' '|| !Alphabet.Contains(x))
                {
                    result += x;
                }
                else
                {
                    var t1 = Alphabet.IndexOf(x) + key;
                    var t2 = t1 % 26;
                    if (t2 < 0)
                    {
                        t2 = 26 + t2;
                    }
                    result += keywordAlphabet[t2];
                }
            }

            return result;
        }

        private static string Vigener(string inputText, string keyword)
        {
            var result = "";

            var j = 0;
            foreach (var x in inputText)
            {
                if (x == ' ')
                {
                    result += " ";
                }
                else
                {
                    var t1 = Alphabet.IndexOf(x)+ Alphabet.IndexOf(keyword[j]);
                    var t2 = t1 % 26;
                    if (t2 < 0)
                    {
                        t2 = 26 + t2;
                    }
                    result += Alphabet[t2];
                    j = (j + 1) % keyword.Length;
                }
            }
            return result;
        }

        private static string CaesarEditAlphabet(string keyword)
        {
            var word = new List<char>();
            foreach (var letter in keyword)
            {
                if(!word.Contains(letter))
                {
                    word.Add(letter);
                }
            }

            var lAlphabet = new List<char>();
            foreach (var letter in Alphabet)
            {
                lAlphabet.Add(letter);
            }
            var selection = (from x in lAlphabet
                             where !word.Contains(x)
                             select x).ToArray();
            var result = "";
            foreach (var x in word)
            {
                result = string.Concat(result, x.ToString());
            }

            return selection.Aggregate(result, (current, x) => string.Concat(current, x.ToString()));
        }
    }

    internal struct Pair: IComparable<Pair>
    {
        public char Letter;
        public int Counter;

        public int CompareTo(Pair obj)
        {
            return Counter.CompareTo(obj.Counter);
        }

        public static void Sort(List<Pair> list)
        {
            list.Sort((a, b) => b.Counter.CompareTo(a.Counter));
        }
    }
}
