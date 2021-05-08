namespace RSA
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;

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
            var n = 23393;
            var primeNumbers = GetPrimeNumbers(n);
            var p = primeNumbers[0];
            var q = primeNumbers[1];
            var b = 12187;
            var fn = (p - 1) * (q - 1);
            // This is the secret key
            var a = ModInverse(b, fn);
            Console.WriteLine("The private key is: " + a);
            var textToDecode = @"12092 3752 12661 828 5876 2958 17624 20500 2958 8236 20022 18911 2360 868
                                 11732 11891 412 19177 8236 12803 21844 6741 14266 17574 9352 17574 8411
                                 20211 2360 6741 14266 17574 9352 17624 20500 2958 8236 3915 18731 8566
                                 10736 5667 6879 15574 20192 2360 868 12734 6299 16775 13021 10167 18788
                                 18096 2360 8236 17574 12803 21844 12803 21844";
            var words = textToDecode.Split(new char[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                var code = int.Parse(word);
                var r = BigInteger.ModPow(code, a, n);
                PrintWord((int)r);
            }
        }

        private static void PrintWord(int encodedWord)
        {
            var a = encodedWord / 26;
            var b = a / 26;
            var c = a % 26;
            var d = encodedWord % 26;

            Console.Write("" + alphabet[b] + alphabet[c] + alphabet[d]);
        }


        private static List<int> GetPrimeNumbers(int number)
        {
            var primes = new List<int>();

            for (int div = 2; div <= number; div++)
                while (number % div == 0)
                {
                    primes.Add(div);
                    number = number / div;
                }

            return primes;
        }

        private static int ModInverse(int a, int m)
        {
            if (Gcd(a, m) != 1)
            {
                return -1;
            }

            int x;
            for (x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                {
                    break;
                }
            }

            return x;
        }

        private static int Gcd(int r, int s)
        {
            while (s != 0)
            {
                int t = s;
                s = r % s;
                r = t;
            }
            return r;
        }
    }
}
