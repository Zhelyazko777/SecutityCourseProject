namespace ElGamal
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
            var p = 29683;
            var alpha = FindTenthPrimitiveRoot(p);
            var b = 15540;
            // This is the secret key
            var a = CalcSecretKey(alpha, b, p);
            Console.WriteLine("The private key is: " + a);
            var textToDecrypt = @"(23234,18606) (12089,4286) (28242,27890) (9945,17970) (1727,23951)
                                (10559,7762) (805,25691) (27862,18325) (4695,12512) (12229,13895)
                                (12946,29020) (3777,6116) (27620,5018) (10389,19448) (15857,14304)
                                (1126,20170) (2551,26375) (26907,20807) (12021,302) (29321,28583)
                                (8195,18546) (14709,2582) (12213,17624) (4570,18816) (14732,17535)
                                (12367,17927) (14675,4587) (2734,26113) (7417,21403) (1431,17314)
                                (27865,5418) (23964,17061) (11465,17396) (10330,28509) (432,25763)
                                (19170,5315) (3800,1607) (8117,2869) (28148,27198) (8812,8230) (25652,27569)
                                (24624,23850) (456,28056) (26253,11316) (23171,18010) (26916,17632)
                                (16853,25102) (11363,12011) (12253,1048) (4667,18719) (3364,17995)
                                (26502,12122) (23680,28954) (24569,4596) (27196,26674) (19605,10484)
                                (15579,5124) (20462,4941) (26721,2938) (19964,7326) (19291,16068)";
            var words = textToDecrypt.Split(new char[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                var result = Decrypt(word, a, p, alpha, b);
                PrintWord(result);
            }
        }

        private static long Decrypt(string word, int a, int p, int alpha, int b)
        {
            var trimedWord = word.Trim(new char[] { '(', ')' });
            var wordParams = trimedWord.Split(",");
            var y1 = int.Parse(wordParams[0]);
            var y2 = int.Parse(wordParams[1]);
            
            return y2 * ModInverse((long)BigInteger.ModPow(y1, a, p), p) % p;
        }

        private static void PrintWord(long encodedWord)
        {
            var a = encodedWord / 26;
            var b = a / 26;
            var c = a % 26;
            var d = encodedWord % 26;

            Console.Write("" + alphabet[(int)b] + alphabet[(int)c] + alphabet[(int)d]);
        }

        private static int CalcSecretKey(int alpha, int b, int mod)
        {
            for (int i = 1; i <= mod; i++)
            {
                var res = BigInteger.ModPow(alpha, i, mod);
                if (res == b)
                {
                    return i;
                }
            }

            return -1;
        }

        private static int FindTenthPrimitiveRoot(int mod)
        {
            var n = mod - 1;
            var primeNumbers = GetPrimeNumbers(n);
            var counter = 0;

            for (int i = 2; i < n; i++)
            {
                var isPrimitiveRoot = true;
                foreach (var num in primeNumbers)
                {
                    if (BigInteger.Pow(i, n / num) % mod == 1)
                    {
                        isPrimitiveRoot = false;
                    }
                }
                if (isPrimitiveRoot)
                {
                    if (++counter == 10)
                    {
                        return i;
                    }
                }
            }

            return -1;
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

        private static long ModInverse(long a, long m)
        {
            if (Gcd(a, m) != 1)
            {
                return -1;
            }

            long x;
            for (x = 1; x < m; x++)
            {
                if ((a * x) % m == 1)
                {
                    break;
                }
            }

            return x;
        }

        private static long Gcd(long r, long s)
        {
            while (s != 0)
            {
                long t = s;
                s = r % s;
                r = t;
            }
            return r;
        }
    }
}
