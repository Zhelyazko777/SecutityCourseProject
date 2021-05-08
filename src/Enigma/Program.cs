namespace Enigma
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

        private static List<int> pis = new List<int>()
        {
            9,
            17,
            18,
            8,
            25,
            14,
            13,
            21,
            4,
            16,
            20,
            24,
            10,
            3,
            6,
            2,
            0,
            12,
            5,
            1,
            7,
            15,
            19,
            11,
            23,
            22,
        };



        public static void Main()
        {

            var stringToDecrypt = "XVGQUJDOHWLHJHSWUOPMOZTEXMBXGABYAEBLPIGRBFQNZXIHQEFRXUEIICNGVKGEGSPNRYWRCPAUVSUYVFJJDEBDDFROMBUPWRYSQBLPAXJHSRAOPBPFFOMMKVFJYSTQSEUUDBBYIMMGRKZOKMKVZMOAXV";
            for (var k = 0; k < alphabet.Count; k++)
            {
                var decryptedChars = new List<char>();
                for (var j = 0; j < stringToDecrypt.Length; j++) 
                {
                    var letter = stringToDecrypt[j];
                    var letterIndexInAlphabet = alphabet.IndexOf(letter);
                    // We use here j directly instead of first to do sth like i = j + 1 and then i - 1
                    var z = (k + j) % 26;
                    var substr = (letterIndexInAlphabet - z) % 26;
                    // When the result is negative we should make it positive by module 26
                    if (substr < 0)
                    {
                        substr = 26 + substr;
                    }
                    var decryptedLetter = alphabet[pis.IndexOf(substr)];
                    decryptedChars.Add(decryptedLetter);
                }

                Console.WriteLine($"With key: {k}");
                Console.WriteLine(String.Join("", decryptedChars));
            }

        }
    }
}
