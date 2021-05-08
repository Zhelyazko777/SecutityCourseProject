namespace Square
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            var textToDecrypt = "SWEOTWIREENUHEVFVLTRRLEIEVWOEVFVNEONEEOETTFETFUTHXOLTTVERNUVWWEEEIREOONFENTOOETOSEWNNLHUIFEEEERRTESIEVIRWLEVNENTEEVETOEHLVESWNFRVENEEEOEENFVLNUE";
            var used = new List<string>();
            for (int i = 1; i <= Math.Sqrt(textToDecrypt.Length); i++)
            {
                for (int j = 1; j <= Math.Sqrt(textToDecrypt.Length); j++)
                {
                    if (used.Any(x => x == $"{i}:{j}"))
                    {
                        continue;
                    }

                    Decrypt(textToDecrypt, i, j);
                    used.Add($"{i}:{j}");
                    if (i != j)
                    {
                        Decrypt(textToDecrypt, j, i);
                        used.Add($"{j}:{i}");
                    }
                }
            }
        }

        private static void Decrypt(string text, int m, int n)
        {
            var matrix = new List<List<List<char>>>();
            var chunkSize = m * n;
            var numberOfChunks = text.Length / chunkSize;
            for (int i = 0; i < numberOfChunks; i++)
            {
                if (chunkSize > text.Length)
                {
                    chunkSize = text.Length - 1;
                }

                var chunkStr = text.Substring(0, chunkSize);
                text = text.Remove(0, chunkStr.Length);
                var chunksList = new List<List<char>>();
                for (int j = 0; j < n; j++)
                {
                    var colList = new List<char>();
                    for (int k = 0; k < m; k++)
                    {
                        colList.Add(chunkStr[k]);
                    }

                    chunkStr = chunkStr.Remove(0, m);
                    chunksList.Add(colList);
                }
                matrix.Add(chunksList);
            }

            Print(matrix, m, n);
        }

        private static void Print(List<List<List<char>>> matrix, int m, int n)
        {
            var result = "";
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        result += matrix[i][k][j];
                    }
                }
            }

            Console.WriteLine($"With m={m} and n={n}:");
            Console.WriteLine(result);
        }
    }
}
