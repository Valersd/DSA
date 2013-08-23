using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;
using System.IO;

namespace _3.SupermarketQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MarketQueue mq = new MarketQueue();
                while (true)
                {
                    string command = Console.ReadLine();
                    if (command == "End")
                    {
                        Console.Write(mq.Output);
                        return;
                    }
                    mq.ProceedCommand(command);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
    public class MarketQueue
    {
        private Dictionary<string, int> byName;
        private BigList<string> queue;
        public StringBuilder Output;
        public MarketQueue()
        {
            this.byName = new Dictionary<string, int>();
            this.queue = new BigList<string>();
            this.Output = new StringBuilder();
        }
        private void Append(string name)
        {
            AppendToDictionary(name);
            queue.Add(name);
            this.Output.AppendLine("OK");
        }
        private void AppendToDictionary(string name)
        {
            if (!byName.ContainsKey(name))
            {
                byName.Add(name, 1);
            }
            else
            {
                byName[name]++;
            }
        }
        private void Insert(string data)
        {
            string[] positionAndName = data.Split();
            int position = int.Parse(positionAndName[0]);
            string name = positionAndName[1];
            if (position > queue.Count || position < 0)
            {
                this.Output.AppendLine("Error");
            }
            else if (position == queue.Count)
            {
                this.Append(name);
            }
            else
            {
                AppendToDictionary(name);
                queue.Insert(position, name);
                this.Output.AppendLine("OK");
            }
        }
        private void Find(string name)
        {
            int count;
            if (byName.TryGetValue(name,out count))
            {
                this.Output.AppendLine(count.ToString());
            }
            else
            {
                this.Output.AppendLine("0");
            }
        }
        private void Serve(string countStr)
        {
            int count = int.Parse(countStr);
            if (count > queue.Count || count < 0)
            {
                this.Output.AppendLine("Error");
            }
            else
            {
                IEnumerable<string> servedPersons = queue.Range(0, count);
                this.Output.AppendLine(String.Join(" ", servedPersons));
                foreach (string name in servedPersons)
                {
                    if (byName[name] > 1)
                    {
                        byName[name]--;
                    }
                    else
                    {
                        byName.Remove(name);
                    }
                }
                queue.RemoveRange(0, count);
            }
        }
        public void ProceedCommand(string commandLine)
        {
            int indexWhitespace = commandLine.IndexOf(' ');
            string command = commandLine.Substring(0, indexWhitespace);
            string data = commandLine.Substring(indexWhitespace + 1);
            switch (command[0])
            {
                case 'A': Append(data); break;
                case 'I': Insert(data); break;
                case 'F': Find(data); break;
                case 'S': Serve(data); break;
                default: throw new ArgumentException("Invalid command");
            }
        }
    }
}
