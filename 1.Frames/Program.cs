using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _1.Frames
{
    class Program
    {
        static KeyValuePair<int,int>[] frames;
        static SortedSet<string> permutations = new SortedSet<string>();
        static void Main(string[] args)
        {
            try
            {
                int n = int.Parse(Console.ReadLine());
                frames = new KeyValuePair<int,int>[n];
                string[] result = new string[n];
                for (int i = 0; i < n; i++)
                {
                    string[] frameStr = Console.ReadLine().Split();
                    KeyValuePair<int, int> frame = new KeyValuePair<int, int>(int.Parse(frameStr[0]), int.Parse(frameStr[1]));
                    frames[i] = frame;
                }
                GetPermutations(n - 1);
                Console.WriteLine(permutations.Count);
                foreach (string permutation in permutations)
                {
                    Console.WriteLine(permutation);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        static void GetPermutations(int k)
        {
            if (k == 0)
            {
                GetCombinations(frames);
                return;
            }
            for (int i = 0; i <= k; i++)
            {
                KeyValuePair<int,int> temp = new KeyValuePair<int,int>(frames[k].Key,frames[k].Value);
                frames[k] = frames[i];
                frames[i] = temp;
                GetPermutations(k - 1);
                temp = frames[k];
                frames[k] = frames[i];
                frames[i] = temp;
            }
        }
        static KeyValuePair<int, int> Reverse(KeyValuePair<int, int> input)
        {
            KeyValuePair<int, int> result = new KeyValuePair<int, int>(input.Value, input.Key);
            return result;
        }
        static void GetCombinations(KeyValuePair<int, int>[] source)
        {
            KeyValuePair<int, int>[] temp = new KeyValuePair<int, int>[source.Length];
            int[] combinations = new int[(int)Math.Pow(2, source.Length)];
            for (int i = 0; i < combinations.Length; i++)
            {
                Array.Copy(source, temp, source.Length);
                for (int j = 0; j < source.Length; j++)
                {
                    if ((i & 1<<j) != 0)
                    {
                        temp[j] = Reverse(source[j]);
                    }
                    else
                    {
                        temp[j] = source[j];
                    }
                }
                permutations.Add(PrintArray(temp));
            }
        }
        static string PrintArray(KeyValuePair<int, int>[] array)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.Length - 1; i++)
            {
                string frame = "(" + array[i].Key + ", " + array[i].Value + ")";
                sb.Append(frame + " | ");
            }
            sb.Append("(" + array[array.Length - 1].Key + ", " + array[array.Length - 1].Value + ")");
            return sb.ToString();
        }
    }
}
