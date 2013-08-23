using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2._3DLabyrinth
{
    class Program
    {
        static char[, ,] labirint;
        static int levels = 0;
        static int rows = 0;
        static int cols = 0;
        static bool[, ,] visited;
        static Queue<int> que = new Queue<int>();
        static void Main(string[] args)
        {
            try
            {
                string[] startStr = Console.ReadLine().Split();
                int startL = int.Parse(startStr[0]);
                int startR = int.Parse(startStr[1]);
                int startC = int.Parse(startStr[2]);
                string[] labirintStr = Console.ReadLine().Split();
                levels = int.Parse(labirintStr[0]);
                rows = int.Parse(labirintStr[1]);
                cols = int.Parse(labirintStr[2]);
                labirint = new char[levels, rows, cols];
                int lev = 0;
                int row = 0;
                while (lev < levels)
                {
                    string data = Console.ReadLine();
                    for (int col = 0; col < data.Length; col++)
                    {
                        labirint[lev, row, col] = data[col];
                    }
                    row++;
                    if (row == rows)
                    {
                        row = 0;
                        lev++;
                    }
                }
                que.Enqueue(startL);
                que.Enqueue(startR);
                que.Enqueue(startC);
                que.Enqueue(0);
                while (que.Count > 0)
                {
                    CheckAndEnqueue(que.Dequeue(), que.Dequeue(), que.Dequeue(), que.Dequeue());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        static void CheckAndEnqueue(int lev, int row, int col, int step)
        {
            if (lev < 0 || lev >= levels)
            {
                Console.WriteLine(step);
                Environment.Exit(0);
            }
            if (row < 0 || col < 0 || row >= rows || col >= cols || labirint[lev,row,col] == '#')
            {
                return;
            }
            if (labirint[lev,row,col] != '#')
            {
                que.Enqueue(lev);
                que.Enqueue(row - 1);
                que.Enqueue(col);
                que.Enqueue(step + 1);
                que.Enqueue(lev);
                que.Enqueue(row + 1);
                que.Enqueue(col);
                que.Enqueue(step + 1);
                que.Enqueue(lev);
                que.Enqueue(row);
                que.Enqueue(col - 1);
                que.Enqueue(step + 1);
                que.Enqueue(lev);
                que.Enqueue(row);
                que.Enqueue(col + 1);
                que.Enqueue(step + 1);
                if (labirint[lev, row, col] == 'U')
                {
                    que.Enqueue(lev + 1);
                    que.Enqueue(row);
                    que.Enqueue(col);
                    que.Enqueue(step + 1);
                }
                else if (labirint[lev, row, col] == 'D')
                {
                    que.Enqueue(lev - 1);
                    que.Enqueue(row);
                    que.Enqueue(col);
                    que.Enqueue(step + 1);
                }
                labirint[lev, row, col] = '#';
            }
        }
    }
}
