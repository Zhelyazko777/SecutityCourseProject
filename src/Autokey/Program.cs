namespace Autokey
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        private static IList<char> alphabet = new List<char>()
        {
            'A',
            'B',
            'C',
            'D',
            'E',
            'F',
            'G',
            'H',
            'I',
            'J',
            'K',
            'L',
            'M',
            'N',
            'O',
            'P',
            'Q',
            'R',
            'S',
            'T',
            'U',
            'V',
            'W',
            'X',
            'Y',
            'Z',
        };

        public static void Main()
        {
            var text = Console.ReadLine();
            for (int i = 0; i < alphabet.Count; i++)
            {
                Decrypt(text, i);
            }
        }

        private static void Decrypt(string text, int key)
        {
            var textToDecryptIndexes = FindTextAlphabetIndexes(text);
            var result = new List<int>();
            int r = 0;
            for (int i = 0; i < textToDecryptIndexes.Count; i++)
            {
                if (i == 0)
                {
                    r = calculateByModule26(textToDecryptIndexes[i], key);
                    result.Add(r);
                    continue;
                }

                r = calculateByModule26(textToDecryptIndexes[i], r);
                result.Add(r);
            }

            Console.WriteLine($"Decrypted with key {key}: ");
            result.ForEach(i => Console.Write(alphabet[i]));
            Console.WriteLine();
        }

        private static IList<int> FindTextAlphabetIndexes(string text)
        {
            var result = new List<int>();
            foreach (var letter in text)
            {
                result.Add(alphabet.IndexOf(letter));
            }

            return result;
        }

        private static int calculateByModule26(int a, int b)
        {
            if (a - b < 0)
            {
                return 26 + (a - b);
            }

            return a - b;
        }
    }
}
