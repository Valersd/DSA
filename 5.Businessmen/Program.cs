using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _5.Businessmen
{
    class Program
    {
        static long[] shakes = new long[71];
        static void Main(string[] args)
        {
            try
            {
                int n = int.Parse(Console.ReadLine());
                shakes[0] = 1;
                shakes[2] = 1;
                shakes[4] = 2;
                shakes[6] = 5;
                Console.WriteLine(GetShake(n));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        static long GetShake(int n)
        {
            if (shakes[n] != 0)
            {
                return shakes[n];
            }
            long result = 0;
            for (int i = 0; i <= n - 2; i+=2)
            {
                result += GetShake(i) * GetShake(n - 2 - i);
            }
            shakes[n] = result;
            return result;
        }
    }
}
