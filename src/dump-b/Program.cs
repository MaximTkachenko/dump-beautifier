using System;
using System.IO;

namespace dump_b
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("provide path to dump file");
                return;
            }

            var path = args[0];
            if (!File.Exists(path))
            {
                Console.WriteLine("file not found");
                return;
            }

            //start processing
            //todo 1. parse threads
            //todo 2. generate html file
            //todo 3. parse heap
        }
    }
}
