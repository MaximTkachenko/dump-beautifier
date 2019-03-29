using System;
using System.IO;

namespace dump_b
{
    //https://blog.maartenballiauw.be/post/2017/01/03/exploring-.net-managed-heap-with-clrmd.html
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

            using (var html = new HtmlGenerator(path))
            using (var parser = new DumpParser(path))
            {
                html.RenderThreads(parser.GetThreads());
                html.RenderHeap(parser.GetHeap());
            }
        }
    }
}
