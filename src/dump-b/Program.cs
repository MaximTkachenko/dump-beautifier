using System;
using System.IO;

namespace dump_b
{
    //https://blog.maartenballiauw.be/post/2017/01/03/exploring-.net-managed-heap-with-clrmd.html
    // https://github.com/Microsoft/clrmd/blob/master/Documentation/GettingStarted.md
    // https://labs.criteo.com/2017/02/going-beyond-sos-clrmd-part-1/
    class Program
    {
        static void Main(string[] args)
        {
            args = new[] { @"C:\code\dumps\Wavecell.MessageSphere.exe_190203_090555.dmp" };
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
